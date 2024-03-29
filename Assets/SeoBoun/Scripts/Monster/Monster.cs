using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

// ������ �⺻ ����
public enum States { Idle, Trace, Attack, Return, Size }

// ������ ������ ���� �ʿ���. -> ���� �߿��� Trace��, ������ �Ұ����ϸ� Ư�� ��Ÿ���� ����Ϸ��� �ʼ� �׸�������..?
public enum AttackStates { BeginAttack, Attacking, EndAttacking, Size}
public class Monster : MonoBehaviour
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
    [SerializeField] float damage;          // ������

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
        // ������ Idle
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
        // ���� ������ -> Ŭ���� �ʸ� ����Ͽ� �ڷ�ƾ �������� �ϴ� ���� ��������
        attackState = AttackStates.BeginAttack;

        if (isAttackCoolTime)
        {
            Debug.Log("���� ��Ÿ����");
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
        Debug.Log("���� ��Ÿ�� ��");
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