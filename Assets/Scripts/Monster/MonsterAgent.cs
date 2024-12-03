using UnityEngine;
using UnityEngine.AI;

public abstract class MonsterAgent : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected GameObject player;
    protected Animator animator;
    protected HpController hpController;

    protected Vector3 direction;
    protected Vector3 distanceToTarget;

    public enum MonsterState
    {
        Idle,
        Move,
        Attack,
        Hit,
        Death,
    }
    protected MonsterState state;

    //Monster Stat
    protected float moveSpeed;
    protected float attackRange;
    protected float attackDamage;
    protected int attackVarious;

    protected float attackDelay;
    public float attackTimer = 0f;

    protected int hp;
    public MonsterStat monsterStat;


    private void Awake()
    {
        //NavMesh Agent
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //Animator
        animator = GetComponent<Animator>();
        hpController = GetComponent<HpController>();
    }

    protected virtual void Update()
    {
        UpdatePlayerReference();
        HandleTransform();
        HandleState();
    }
    protected void UpdatePlayerReference()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            agent.destination = player.transform.position;
            distanceToTarget = agent.destination - transform.position;
        }
    }
    protected void HandleTransform()
    {
        if (player != null)
        {
            animator.SetFloat("MoveSpeed", agent.speed);
            direction = player.transform.position - transform.position;
        }
        if (state==MonsterState.Move)
        {
            if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    protected abstract void HandleState();
    protected virtual void HandleMoveState()
    {
        if (CheckAttackDelay())
        {
            if (distanceToTarget.magnitude < attackRange)
            {
                attackTimer = 0f;
                int rand = Random.Range(0, attackVarious);
                animator.SetFloat("AttackType", (float)rand);
                animator.SetTrigger("Attack");
                state = MonsterState.Attack;
            }
        }
    }
    protected virtual void HandleAttackState()
    {
        agent.speed = 0f;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetTrigger("Idle");
            agent.speed = moveSpeed;
            state = MonsterState.Move;
        }
    }
    protected virtual void HandleHpState()
    {
        animator.SetTrigger("Hit");
        state = MonsterState.Hit;
    }
    protected virtual void HandleHitState()
    {
        agent.speed = 0f;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            && animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            if (hpController.hp < 0.1f)
            {
                animator.SetTrigger("Death");
                state = MonsterState.Death;
            }
            else
            {
                animator.SetTrigger("Idle");
                agent.speed = moveSpeed;
                state = MonsterState.Move;
            }
        }
    }
    protected virtual void HandleDeathState()
    {
        agent.speed = 0f;
        GetComponent<Collider2D>().enabled = false;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        hpController.OnHpChanged -= HandleHpState;
    }
    public bool CheckAttackDelay()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            return true;      
        }
        return false;
    }
}