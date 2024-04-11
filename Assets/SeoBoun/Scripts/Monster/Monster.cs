using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
// ������ �⺻ ����
public enum States { Idle, Trace, Attack, Return, Patrol, Die, Size }

// ������ ������ ���� �ʿ���. -> ���� �߿��� Trace��, ������ �Ұ����ϸ� Ư�� ��Ÿ���� ����Ϸ��� �ʼ� �׸�������..?
public enum AttackStates { BeginAttack, Attacking, EndAttacking, Size}
public class Monster : PooledObject, IDamagable
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] MonsterFov monsterFov;
    [SerializeField] AnimationClip attackClip;
    [SerializeField] Transform playerTransform;

    [SerializeField] SkinnedMeshRenderer[] meshRenderer;

    [Header("ZombieStat")]
    [SerializeField] int hp;                // hp 
    [SerializeField] int moveSpeed;         // �̵��ӵ�
    [SerializeField] int targetSpeed;       // TODO.. Trace, Idle �̵��ӵ� �����ϱ�
    [SerializeField] float findRange;       // Ž�� ����
    [SerializeField] float attackRange;     // ���� ����   
    [SerializeField] float attackRate;      // ���� ��Ÿ��? ��?
    [SerializeField] int damage;            // ������ TODO.. IDamagable or LivingClass�� ���� ����

    [SerializeField] Vector3 startPos;
    [SerializeField] LayerMask playerLayer;

    StatesMachine fsm;

    public States curState = new States();
    public AttackStates attackState = new AttackStates();

    bool isAttackCoolTime = false;
    bool isAttacking = false;
    float cosRange;

    Collider[] colliders = new Collider[5];

    public Animator Animator { get => animator;}
    public bool IsAttack { get => isAttackCoolTime; }

    private void Awake()
    {
        cosRange = Mathf.Cos(Mathf.Deg2Rad * 45);
    }

    private void OnDisable()
    {
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            meshRenderer[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        // ������ Idle
        startPos = transform.position;
    }

    public void Init(ZombieData zombieData)
    {
        int rand = Random.Range(0, meshRenderer.Length);

        hp = zombieData.hp;
        moveSpeed = zombieData.moveSpeed;
        targetSpeed = zombieData.targetSpeed;
        findRange = zombieData.findRange;
        attackRange = zombieData.attackRange;
        attackRate = zombieData.attackRate;
        damage = zombieData.damage;
        meshRenderer[rand].gameObject.SetActive(true);
        meshRenderer[rand].material = zombieData.zombieMaterial;
        playerTransform = Manager.Game.playerPos;
        fsm = new StatesMachine();
        fsm.Init(this, States.Idle);
        curState = States.Idle;
    }

    private void Update()
    {
        fsm.Update();
    }

    public void Targeting()
    {
        agent.speed = targetSpeed;
    }

    public void Patrol()
    {
        agent.speed = moveSpeed;
    }

    public void Attack()
    {// ���� ���÷�ƾ ����
        int size = Physics.OverlapSphereNonAlloc(transform.position, 3f, colliders, playerLayer);

        for(int i = 0; i < size; i++)
        {
            Vector3 dirToTarget = (colliders[i].transform.position - transform.position).normalized;
            if(Vector3.Dot(transform.forward, dirToTarget) < cosRange)
            {
                continue;
            }

            IDamagable target = colliders[i].gameObject.GetComponent<IDamagable>();

            target?.TakeHit((int)damage);
            break;

        }
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
            animator.SetTrigger("Hit");
        }

        return true;
    }


    public void ReturnPool()
    {
        SetAutoRelease();
    }

    Coroutine traceRoutine;
    public void StartTrace()
    {
        traceRoutine = StartCoroutine(TraceRoutine());
    }

    public void EndTrace()
    {
        StopCoroutine(traceRoutine);
    }

    IEnumerator TraceRoutine()
    {
        while (true)
        {
            agent.SetDestination(playerTransform.position);
            yield return new WaitForSeconds(1f);
        }
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
            owner.Animator.SetFloat("IsWalk", 0.0f);
        }

        public override void Transition()
        {
            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) < owner.findRange && owner.monsterFov.isFind)
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
            owner.Targeting();
            owner.StartTrace();
            owner.Animator.SetFloat("IsWalk", owner.agent.speed);
        }

        public override void Transition()
        {
            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) < owner.attackRange)
            {
                owner.curState = States.Attack;
                fsm.ChangeState(States.Attack);
            }

            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) > owner.findRange && !owner.monsterFov.isFind)
            {
                owner.curState = States.Return;
                fsm.ChangeState(States.Return);
            }
        }

        public override void Exit()
        {
            owner.Patrol();
            owner.EndTrace();
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
            owner.Animator.SetFloat("IsWalk", owner.agent.speed);
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
                if (owner.monsterFov.isFind)  // Ÿ���� �þ߾ȿ� ���� ��
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

        public override void Exit()
        {
            owner.Animator.SetTrigger("Walk");
        }
    }

    private class ReturnState : BaseState
    {
        public ReturnState(StatesMachine fsm, Monster owner) : base(fsm, owner) { }

        public override void Enter()
        {
            owner.agent.SetDestination(owner.startPos);
        }

        public override void Transition()
        {
            if (Vector3.Distance(owner.transform.position, owner.startPos) < 4f && !owner.monsterFov.isFind)
            {
                owner.curState = States.Idle;
                fsm.ChangeState(States.Idle);
            }

            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) < owner.findRange && owner.monsterFov.isFind)
            {
                owner.curState = States.Trace;
                fsm.ChangeState(States.Trace);
            }
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
            owner.agent.isStopped = true;
            owner.ReturnPool();
        }
    }
    #endregion
}