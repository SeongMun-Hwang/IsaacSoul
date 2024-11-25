using UnityEngine;
using UnityEngine.AI;

public class MonsterAgent : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected GameObject player;
    protected Animator animator;
    protected Vector3 direction;
    protected Vector3 distanceToTarget;

    public enum MonsterState
    {
        Idle,
        Move,
        Attack,
    }
    private void Awake()
    {
        //NavMesh Agent
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //Animator
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            agent.destination = player.transform.position;
            distanceToTarget = agent.destination - transform.position;
            animator.SetFloat("MoveSpeed", agent.speed);
            direction = player.transform.position - transform.position;
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
}