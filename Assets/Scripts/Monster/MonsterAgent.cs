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
    protected float attackTimer = 0f;

    protected int hp;

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
        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    protected abstract void HandleState();
}