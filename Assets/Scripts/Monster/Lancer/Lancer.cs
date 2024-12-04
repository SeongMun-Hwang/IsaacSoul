using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Lancer : MonsterAgent
{
    bool stopDash = false;
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
        if (player != null)
        {
            if (Mathf.Abs(player.transform.position.y - transform.position.y) > 2f)
            {
                agent.destination=new Vector2(transform.position.x, player.transform.position.y);
            }
            else
            {
                agent.destination = player.transform.position;
            }
            distanceToTarget = agent.destination - transform.position;
        }
    }
    protected override void HandleMoveState()
    {
        if (CheckAttackDelay())
        {
            attackTimer = 0f;
            if (distanceToTarget.magnitude < attackRange)
            {
                int rand = Random.Range(0, attackVarious);
                animator.SetFloat("AttackType", (float)rand);
                animator.SetTrigger("Attack");
                state = MonsterState.Attack;
            }
            else
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                Vector2 dashDirection = new Vector2(rb.linearVelocityX,0f);
                //rb.AddForce(dashDirection * 10,ForceMode2D.Impulse);
                animator.SetFloat("AttackType", 2);
                agent.speed *= 100;
                animator.SetTrigger("Attack");
                state = MonsterState.Attack;
            }
        }
    }
    protected override void HandleAttackState()
    {
        if (animator.GetFloat("AttackType") != 2)
        {
            agent.speed = 0f;
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.SetTrigger("Idle");
                agent.speed = moveSpeed;
                state = MonsterState.Move;
            }
        }
        else
        {
            if (stopDash)
            {
                stopDash = false;
                animator.SetTrigger("Idle");
                agent.speed = moveSpeed;
                state = MonsterState.Move;
            }
        }
    }
    protected override void HandleHpState()
    {
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
    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            stopDash = true;
            transform.GetComponent<Rigidbody2D>().linearVelocity= Vector3.zero;
            agent.speed = 0;
        }
    }
}
