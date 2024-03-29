using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

// 좀비의 기본 상태
public enum States { Idle, Trace, Attack, Return, Size }

// 어택은 구분이 조금 필요함. -> 공격 중에는 Trace도, 무엇도 불가능하며 특히 쿨타임을 계산하려면 필수 항목일지도..?
public enum AttackStates { BeginAttack, Attacking, EndAttacking, Size}
public class Monster : MonoBehaviour
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
    [SerializeField] float damage;          // 데미지

    [SerializeField] Vector3 startPos;
    [SerializeField] LayerMask playerLayer;

    StatesMachine fsm;

    public States curState = new States();
    public AttackStates attackState = new AttackStates();

    bool isAttackCoolTime = false;

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
            Debug.Log("어택 쿨타임중");
            return;
        }

        StartCoroutine(AttackCoolTime());
    }
    
    IEnumerator Attacking()
    {
        attackState = AttackStates.Attacking;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackClip.length);
    }

    IEnumerator AttackCoolTime()
    {
        isAttackCoolTime = true;

        yield return Attacking();

        yield return new WaitForSeconds(attackRate);
        Debug.Log("어택 쿨타임 끝");
        isAttackCoolTime = false;
        attackState = AttackStates.EndAttacking;
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
        public AttackState(StatesMachine fsm, Monster owner) : base(fsm, owner) { }

        public override void Enter()
        {
            // TODO
            // Animation Play
        }

        public override void Update()
        {
            owner.AttackRoutine();
            owner.agent.isStopped = true;
        }

        public override void Transition()
        {
            if(Vector3.Distance(owner.playerTransform.position, owner.transform.position) > owner.attackRange && owner.monsterFov.FindTarget())
            {
                owner.curState = States.Trace;
                fsm.ChangeState(States.Trace);
            }

            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) > owner.attackRange && !owner.monsterFov.FindTarget())
            {
                owner.curState = States.Return;
                fsm.ChangeState(States.Return);
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
    #endregion
}