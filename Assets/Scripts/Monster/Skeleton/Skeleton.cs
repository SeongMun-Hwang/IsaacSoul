using TMPro;
using UnityEngine;

public class Skeleton : MonsterAgent
{
    public AudioClip attackSound;
    private void Start()
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
    protected override void HandleMoveState()
    {
        if (CheckAttackDelay())
        {
            if (distanceToTarget.magnitude < attackRange)
            {
                int rand = Random.Range(0, attackVarious);
                animator.SetFloat("AttackType", (float)rand);
                animator.SetTrigger("Attack");
                state = MonsterState.Attack;
                monsterSound.PlayOneShot(attackSound);
            }
        }
    }
    protected override void HandleAttackState()
    {
        base.HandleAttackState();
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
}