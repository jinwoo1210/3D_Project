using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Monster : MonsterPooledObject, IDamagable
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] MonsterFov monsterFov;
    [SerializeField] AnimationClip attackClip;
    [SerializeField] Transform playerTransform;

    [SerializeField] SkinnedMeshRenderer[] meshRenderer;

    [Header("ZombieStat")]
    [SerializeField] int hp;                // hp 
    [SerializeField] int moveSpeed;         // 이동속도
    [SerializeField] int targetSpeed;       // TODO.. Trace, Idle 이동속도 변경하기
    [SerializeField] float findRange;       // 탐색 범위
    [SerializeField] float attackRange;     // 공격 범위   
    [SerializeField] float attackRate;      // 어택 쿨타임? 빈도?
    [SerializeField] int damage;            // 데미지 TODO.. IDamagable or LivingClass로 따로 빼기
    [SerializeField] ZombieType type;

    [SerializeField] Vector3 startPos;
    [SerializeField] LayerMask playerLayer;

    StatesMachine fsm;

    public ZombieState curState = new ZombieState();
    public AttackStates attackState = new AttackStates();

    public UnityEvent OnDied;

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
        // 시작은 Idle
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

        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        fsm = new StatesMachine();
        fsm.Init(this, ZombieState.Idle);
        curState = ZombieState.Idle;
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
    {// 실제 어택루틴 실행
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
            fsm.ChangeState(ZombieState.Die);
            OnDied?.Invoke();
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
        private ZombieState curState;
        private BaseState[] states;

        public void Init(Monster owner, ZombieState initState)
        {
            states = new BaseState[(int)ZombieState.Size];

            states[(int)ZombieState.Idle] = new IdleState(this, owner);
            states[(int)ZombieState.Trace] = new TraceState(this, owner);
            states[(int)ZombieState.Attack] = new AttackState(this, owner);
            states[(int)ZombieState.Return] = new ReturnState(this, owner);
            states[(int)ZombieState.Die] = new DieState(this, owner);

            curState = initState;
            states[(int)curState].Enter();
        }

        public void Update()
        {
            states[(int)curState].Update();
            states[(int)curState].Transition();
        }

        public void ChangeState(ZombieState nextState)
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
                owner.curState = ZombieState.Trace;
                fsm.ChangeState(ZombieState.Trace);
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
                owner.curState = ZombieState.Attack;
                fsm.ChangeState(ZombieState.Attack);
            }

            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) > owner.findRange && !owner.monsterFov.isFind)
            {
                owner.curState = ZombieState.Return;
                fsm.ChangeState(ZombieState.Return);
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
        // 1. 조건 검사(사거리 안 + 어택 가능 상태)
        // 2. 어택 시작(Begin)
        //  2-1. 어택 가능하다면 어택 루틴 시작(Attacking)
        //    (어택 중 어느 상태로도 전이 불가)
        //  2-2. 어택 후 어택 쿨타임 시작(AttackCoolTime) -> 끝나면 어택 종료(EndAttack)
        //    (어택은 끝났으니 이동은 가능하되 어택은 불가)
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
            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) > owner.attackRange &&   // 공격거리를 벗어났고,
                !owner.isAttacking)                                                                                 // 공격 중이 아니라면
            {
                owner.agent.isStopped = false;
                if (owner.monsterFov.isFind)  // 타겟이 시야안에 있을 때
                {
                    owner.curState = ZombieState.Trace;
                    fsm.ChangeState(ZombieState.Trace);
                }
                else
                {
                    owner.curState = ZombieState.Return;
                    fsm.ChangeState(ZombieState.Return);
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
                owner.curState = ZombieState.Idle;
                fsm.ChangeState(ZombieState.Idle);
            }

            if (Vector3.Distance(owner.playerTransform.position, owner.transform.position) < owner.findRange && owner.monsterFov.isFind)
            {
                owner.curState = ZombieState.Trace;
                fsm.ChangeState(ZombieState.Trace);
            }
        }
    }

    private class DieState : BaseState
    {
        public DieState(StatesMachine fsm, Monster owner) : base(fsm, owner) { }

        public override void Enter()
        {
            owner.curState = ZombieState.Die;
            owner.animator.SetTrigger("Die");
            owner.GetComponent<CapsuleCollider>().enabled = false;
            owner.GetComponent<BoxCollider>().enabled = false;
            owner.agent.isStopped = true;

            owner.ReturnPool();
        }
    }
    #endregion
}