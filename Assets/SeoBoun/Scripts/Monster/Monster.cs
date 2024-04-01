using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

// 좀비의 기본 상태
public enum States { Idle, Trace, Attack, Return, Patrol, Hit, Die, Size }

// 어택은 구분이 조금 필요함. -> 공격 중에는 Trace도, 무엇도 불가능하며 특히 쿨타임을 계산하려면 필수 항목일지도..?
public enum AttackStates { BeginAttack, Attacking, EndAttacking, Size}
public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] MonsterFov monsterFov;
    [SerializeField] AnimationClip attackClip;
    [SerializeField] Transform playerTransform;

    [SerializeField] float hp;              // hp 
    [SerializeField] float moveSpeed;       // 이동속도
    [SerializeField] float targetSpeed;     // TODO.. Trace, Idle 이동속도 변경하기
    [SerializeField] float findRange;       // 탐색 범위
    [SerializeField] float attackRange;     // 공격 범위   
    [SerializeField] float attackRate;      // 어택 쿨타임? 빈도?
    [SerializeField] float damage;          // 데미지 TODO.. IDamagable or LivingClass로 따로 빼기

    [SerializeField] Vector3 startPos;
    [SerializeField] LayerMask playerLayer;

    StatesMachine fsm;
    Coroutine findRoutine;
    Coroutine hitRoutine;

    public States curState = new States();
    public AttackStates attackState = new AttackStates();

    bool isAttackCoolTime = false;
    bool isAttacking = false;

    Collider[] colliders = new Collider[5];

    public Animator Animator { get => animator;}
    public bool IsAttack { get => isAttackCoolTime; }

    private void Start()
    {
        // 시작은 Idle
        fsm = new StatesMachine();
        fsm.Init(this, States.Idle);
        curState = States.Idle;
        startPos = transform.position;
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        fsm.Update();
    }

    private void AttackRoutine()
    {
        // 어택 나누기 -> 클립의 초를 계산하여 코루틴 연장으로 일단 쉽게 진행하자
        attackState = AttackStates.BeginAttack;

        if (isAttackCoolTime)
        {
            attackState = AttackStates.Attacking;
            return;
        }

        StartCoroutine(AttackCoolTime());
    }

    IEnumerator Attacking()
    {
        // 어택 시작
        isAttacking = true;
        attackState = AttackStates.Attacking;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackClip.length);
        isAttacking = false;
    }

    IEnumerator AttackCoolTime()
    {
        isAttackCoolTime = true;

        yield return Attacking();

        yield return new WaitForSeconds(attackRate);
        isAttackCoolTime = false;
        attackState = AttackStates.EndAttacking;
    }

    public bool TakeHit(int damage)
    {
        hp -= damage;
        
        if(hp <= 0)
        {
            hp = 0;
            fsm.ChangeState(States.Die);
        }
        else
        {
            fsm.ChangeState(States.Hit);
            hitRoutine = StartCoroutine(HitRoutine());
        }

        return true;
    }

    IEnumerator HitRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        fsm.ChangeState(States.Idle);
    }

    #region State
    private class StatesMachine
    {
        private States curState;
        private BaseState[] states;

        public void Init(Monster owner, States initState)
        {
            states = new BaseState[(int)States.Size];

            states[(int)States.Idle] = new IdleState(this, owner);
            states[(int)States.Trace] = new TraceState(this, owner);
            states[(int)States.Attack] = new AttackState(this, owner);
            states[(int)States.Return] = new ReturnState(this, owner);
            states[(int)States.Hit] = new HitState(this, owner);
            states[(int)States.Die] = new DieState(this, owner);

            curState = initState;
            states[(int)curState].Enter();
        }

        public void Update()
        {
            states[(int)curState].Update();
            states[(int)curState].Transition();
        }

        public void ChangeState(States nextState)
        {
            Debug.Log($"{curState} -> {nextState}");
            states[(int)curState].Exit();
            curState = nextState;
            states[(int)curState].Enter();
        }
    }
    private class BaseState
    {
        protected StatesMachine fsm;
        protected Monster owner;

        public BaseState(StatesMachine fsm, Monster owner)
        {
            this.fsm = fsm;
            this.owner = owner;
        }
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Transition() { }
        public virtual void Exit() { }
    }
    private class IdleState : BaseState
    {
        public IdleState(StatesMachine fsm, Monster owner) : base(fsm, owner) { }

        public override void Enter()
        {
            // TODO
            // Animation Play
            owner.Animator.SetFloat("IsWalk", 0.0f);
        }

        public override void Transition()
        {
            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) < owner.findRange && owner.monsterFov.FindTarget())
            {
                owner.curState = States.Trace;
                fsm.ChangeState(States.Trace);
            }
        }

        public override void Exit()
        {
            owner.Animator.SetFloat("IsWalk", 1.0f);
        }
    }

    private class TraceState : BaseState
    {
        public TraceState(StatesMachine fsm, Monster owner) : base(fsm, owner) { }

        public override void Enter()
        {
            // TODO
            // Animation Play
            owner.agent.SetDestination(owner.playerTransform.position);
        }

        public override void Update()
        {
            owner.agent.SetDestination(owner.playerTransform.position);
        }

        public override void Transition()
        {
            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) < owner.attackRange)
            {
                owner.curState = States.Attack;
                fsm.ChangeState(States.Attack);
            }

            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) > owner.findRange && !owner.monsterFov.FindTarget())
            {
                owner.curState = States.Return;
                fsm.ChangeState(States.Return);
            }
        }
    }

    private class AttackState : BaseState
    {
        // 1. 조건 검사(사거리 안 + 어택 가능 상태)
        // 2. 어택 시작(Begin)
        //  2-1. 어택 가능하다면 어택 루틴 시작(Attacking)
        //    (어택 중 어느 상태로도 전이 불가)
        //  2-2. 어택 후 어택 쿨타임 시작(AttackCoolTime) -> 끝나면 어택 종료(EndAttack)
        //    (어택은 끝났으니 이동은 가능하되 어택은 불가)
        public AttackState(StatesMachine fsm, Monster owner) : base(fsm, owner) { }

        public override void Enter()
        {
            // TODO
            // Animation Play
            owner.Animator.SetFloat("IsWalk", 1.0f);
        }

        public override void Update()
        {
            owner.AttackRoutine();
            owner.agent.isStopped = true;
        }

        public override void Transition()
        {
            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) > owner.attackRange &&   // 공격거리를 벗어났고,
                !owner.isAttacking)                                                                                 // 공격 중이 아니라면
            {
                owner.agent.isStopped = false;
                if (owner.monsterFov.FindTarget())  // 타겟이 시야안에 있을 때
                {
                    owner.curState = States.Trace;
                    fsm.ChangeState(States.Trace);
                }
                else
                {
                    owner.curState = States.Return;
                    fsm.ChangeState(States.Return);
                }
            }
        }
    }

    private class ReturnState : BaseState
    {
        public ReturnState(StatesMachine fsm, Monster owner) : base(fsm, owner) { }

        public override void Enter()
        {
            // TODO
            // Animation Play
            owner.agent.SetDestination(owner.startPos);
        }

        public override void Transition()
        {
            if (Vector3.Distance(owner.transform.position, owner.startPos) < 0.1f && !owner.monsterFov.FindTarget())
            {
                owner.curState = States.Idle;
                fsm.ChangeState(States.Idle);
            }

            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) < owner.findRange && owner.monsterFov.FindTarget())
            {
                owner.curState = States.Trace;
                fsm.ChangeState(States.Trace);
            }
        }
    }

    private class HitState : BaseState
    {
        public HitState(StatesMachine fsm, Monster owner) : base(fsm, owner) { }

        public override void Enter()
        {
            owner.curState = States.Hit;
            owner.animator.SetTrigger("Hit");
        }
    }

    private class DieState : BaseState
    {
        public DieState(StatesMachine fsm, Monster owner) : base(fsm, owner) { }

        public override void Enter()
        {
            owner.curState = States.Die;
            owner.animator.SetTrigger("Die");
            owner.GetComponent<Collider>().enabled = false;
        }

        public override void Exit()
        {
            Destroy(owner.gameObject, 5f);
        }
    }
    #endregion
}