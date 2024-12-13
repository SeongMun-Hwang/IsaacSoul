using UnityEngine;

public class Slime : MonsterAgent
{
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
}
