using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonArcher : MonsterAgent
{
    float fleeDistance = 5f;
    public Transform firePosition;
    public GameObject arrowPrefab;
    public AudioClip arrowClip;
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
        base.HandleMoveState();
        if (distanceToTarget.magnitude < fleeDistance)
        {
            Vector3 fleeDirection = -distanceToTarget.normalized;
            Vector3 fleePosition = transform.position + fleeDirection * fleeDistance;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(fleePosition, out hit, 5f, NavMesh.AllAreas))
            {
                agent.destination = hit.position;
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
    public void FireArrow()
    {
        monsterSound.PlayOneShot(arrowClip);
        float attackAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject go = Instantiate(arrowPrefab, new Vector2(firePosition.transform.position.x,
            firePosition.transform.position.y), Quaternion.identity);
        go.transform.rotation = Quaternion.Euler(0f, 0f, attackAngle);
    }
}