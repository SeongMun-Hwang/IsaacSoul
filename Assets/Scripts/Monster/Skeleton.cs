using TMPro;
using UnityEngine;

public class Skeleton : MonsterAgent
{
    public TextMeshPro stateText;
    public MonsterStat monsterStat;

    private void Start()
    {
        //Get MonsterStat data
        attackRange = monsterStat.attackRange;
        moveSpeed = monsterStat.moveSpeed;
        agent.speed = monsterStat.moveSpeed;
        attackVarious = monsterStat.attackVarious;
        attackDelay = monsterStat.attackDelay;
        hpController.hp = monsterStat.hp;

        state = MonsterState.Move;
        hpController.OnHpChanged += HandleHpState;
    }
    protected override void HandleState()
    {
        stateText.text = state.ToString() + attackTimer;
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
    private void HandleMoveState()
    {
        if (distanceToTarget.magnitude < attackRange)
        {
            int rand = Random.Range(0, attackVarious);
            animator.SetFloat("AttackType", (float)rand);
            animator.SetTrigger("Attack");
            state = MonsterState.Attack;
        }
    }
    private void HandleAttackState()
    {
        agent.speed = 0f;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetTrigger("Idle");
            agent.speed = moveSpeed;
            state = MonsterState.Move;
        }
    }
    private void HandleHpState()
    {
        if (hpController.hp > 0)
        {
            animator.SetTrigger("Hit");
            state = MonsterState.Hit;
        }
    }
    private void HandleHitState()
    {
        agent.speed = 0f;

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f 
            && animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            if (hpController.hp < 1)
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
    private void HandleDeathState()
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
}