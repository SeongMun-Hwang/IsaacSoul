using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Lancer : MonsterAgent
{
    bool stopDash = true;
    private float attackDirection = 1;

    void Start()
    {
        hpController.OnHpChanged += HandleHpState;
        //Get MonsterStat data
        attackRange = monsterStat.attackRange;
        moveSpeed = monsterStat.moveSpeed;
        agent.speed = monsterStat.moveSpeed;
        attackVarious = monsterStat.attackVarious;
        attackDelay = monsterStat.attackDelay;
        hpController.hp = monsterStat.hp;

        state = MonsterState.Move;
    }
    protected override void HandleState()
    {
        switch (state)
        {
            case MonsterState.Idle:

                break;
            case MonsterState.Move:
                HandleMoveState();
                break;
            case MonsterState.Attack:
                HandleAttackState();
                break;
            case MonsterState.Hit:
                HandleHitState();
                break;
            case MonsterState.Death:
                HandleDeathState();
                break;
        }
    }
    protected override void UpdatePlayerReference()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null && state != MonsterState.Attack)
        {
            if (Mathf.Abs(player.transform.position.y - transform.position.y) > 2f)
            {
                agent.destination = new Vector2(transform.position.x, player.transform.position.y);
            }
            //else if (Mathf.Abs(player.transform.position.x - transform.position.x) < 0.5f)
            //{
            //    agent.destination = transform.position + -5f*new Vector3(distanceToTarget.x,distanceToTarget.y);
            //}
            else
            {
                agent.destination = player.transform.position;
            }
            distanceToTarget = agent.destination - transform.position;
        }
    }
    protected override void HandleTransform()
    {
        base.HandleTransform();
    }
    protected override void HandleMoveState()
    {
        if (CheckAttackDelay())
        {
            attackTimer = 0f;
            if (distanceToTarget.magnitude < attackRange)
            {
                int rand = Random.Range(0, attackVarious);
                animator.SetFloat("AttackType", rand);
                if (rand == 2)
                {
                    agent.enabled = false;
                    stopDash = false;
                    attackDirection = transform.right.x > 0 ? 1 : -1;
                }
                animator.SetTrigger("Attack");
                state = MonsterState.Attack;
            }
            else
            {
                animator.SetFloat("AttackType", 2);

                agent.enabled = false;
                stopDash = false;
                attackDirection = transform.right.x > 0 ? 1 : -1;
            }
            animator.SetTrigger("Attack");
            state = MonsterState.Attack;
        }
    }
    protected override void HandleAttackState()
    {
        if (animator.GetFloat("AttackType") == 2)
        {
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(12 * attackDirection, 0);
        }
        if (stopDash)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.SetTrigger("Idle");
                agent.speed = moveSpeed;
                state = MonsterState.Move;
            }
        }
    }
    protected override void HandleHpState()
    {
        agent.enabled = true;
        stopDash = true;
        base.HandleHpState();
    }
    protected override void HandleHitState()
    {
        base.HandleHitState();
    }
    protected override void HandleDeathState()
    {
        base.HandleDeathState();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (animator.GetFloat("AttackType") == 2)
        {
            stopDash = true;
            attackTimer = 0;
            agent.enabled = true;
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
