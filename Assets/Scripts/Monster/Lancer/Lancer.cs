using UnityEngine;

public class Lancer : MonsterAgent
{
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
            //Debug.Log($"Player: {player}, Destination: {agent.destination}, Distance: {distanceToTarget.magnitude}");

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
        base.HandleMoveState();
        //if (animator.GetFloat("AttackType") == 2)
        //{
        //    agent.speed *= 2;
        //}
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
