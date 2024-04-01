using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

// ������ �⺻ ����
public enum States { Idle, Trace, Attack, Return, Patrol, Hit, Die, Size }

// ������ ������ ���� �ʿ���. -> ���� �߿��� Trace��, ������ �Ұ����ϸ� Ư�� ��Ÿ���� ����Ϸ��� �ʼ� �׸�������..?
public enum AttackStates { BeginAttack, Attacking, EndAttacking, Size}
public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] MonsterFov monsterFov;
    [SerializeField] AnimationClip attackClip;
    [SerializeField] Transform playerTransform;

    [SerializeField] float hp;              // hp 
    [SerializeField] float moveSpeed;       // �̵��ӵ�
    [SerializeField] float targetSpeed;     // TODO.. Trace, Idle �̵��ӵ� �����ϱ�
    [SerializeField] float findRange;       // Ž�� ����
    [SerializeField] float attackRange;     // ���� ����   
    [SerializeField] float attackRate;      // ���� ��Ÿ��? ��?
    [SerializeField] float damage;          // ������ TODO.. IDamagable or LivingClass�� ���� ����

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
        // ������ Idle
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
        // ���� ������ -> Ŭ���� �ʸ� ����Ͽ� �ڷ�ƾ �������� �ϴ� ���� ��������
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
        // ���� ����
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
        // 1. ���� �˻�(��Ÿ� �� + ���� ���� ����)
        // 2. ���� ����(Begin)
        //  2-1. ���� �����ϴٸ� ���� ��ƾ ����(Attacking)
        //    (���� �� ��� ���·ε� ���� �Ұ�)
        //  2-2. ���� �� ���� ��Ÿ�� ����(AttackCoolTime) -> ������ ���� ����(EndAttack)
        //    (������ �������� �̵��� �����ϵ� ������ �Ұ�)
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
            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) > owner.attackRange &&   // ���ݰŸ��� �����,
                !owner.isAttacking)                                                                                 // ���� ���� �ƴ϶��
            {
                owner.agent.isStopped = false;
                if (owner.monsterFov.FindTarget())  // Ÿ���� �þ߾ȿ� ���� ��
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