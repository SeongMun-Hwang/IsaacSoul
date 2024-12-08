using UnityEngine;
using UnityEngine.AI;

public class StoneGolem : MonsterAgent
{
    public GameObject projectilePrefab;
    public Transform firePosition;
    public Animator LaserAnimator;
    public Animator ShockWaveAnimator;
    public Unknown unknown;

    public AudioClip laserCast;
    public AudioClip attack;
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
    protected override void HandleTransform()
    {
        if (state == MonsterState.Move)
        {
            if (player != null)
            {
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
    }
    protected override void HandleMoveState()
    {
        if (CheckAttackDelay())
        {
            if (distanceToTarget.magnitude < attackRange)
            {
                int rand = Random.Range(0, attackVarious);
                animator.SetFloat("AttackType", rand);
                if (rand == 3)
                {
                    agent.isStopped = true;
                }
                animator.SetTrigger("Attack");
                state = MonsterState.Attack;
                rand = -1;
            }
        }
    }
    protected override void HandleHpState()
    {
        monsterSound.PlayOneShot(hitSound);
    }
    public void FireArrow()
    {
        float attackAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject go = Instantiate(projectilePrefab, new Vector2(firePosition.transform.position.x,
            firePosition.transform.position.y), Quaternion.identity);
        go.transform.rotation = Quaternion.Euler(0f, 0f, attackAngle);
    }
    public void LaserCast()
    {
        monsterSound.PlayOneShot(laserCast);
        LaserAnimator.SetTrigger("Laser");
    }
    public void TeleportToPlayer()
    {
        Vector3 targetPosition = player.transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-5f, 5f));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 5f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
    }
    public void UnknownCircleAttack()
    {
        StartCoroutine(unknown.ShootInCircle());
    }
    public void ShockWaveCast()
    {
        ShockWaveAnimator.SetTrigger("ShockWave");
    }
}
