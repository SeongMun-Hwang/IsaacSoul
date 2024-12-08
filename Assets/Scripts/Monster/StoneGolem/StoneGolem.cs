using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StoneGolem : MonsterAgent
{
    public GameObject projectilePrefab;
    public Transform firePosition;
    public Animator LaserAnimator;
    public Animator ShockWaveAnimator;
    public Unknown unknown;

    public AudioClip laserCast;
    public AudioClip attack;

    float maxHp;
    public Image redHpBar;
    public Image yellowHpBar;
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
        maxHp = hpController.hp;
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
            if (GetComponent<Rigidbody2D>().linearVelocity != Vector2.zero)
            {
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
        float currentHpPercentage = hpController.hp / maxHp;
        StartCoroutine(UpdateHpBars(currentHpPercentage));
        monsterSound.PlayOneShot(hitSound);
        if (hpController.hp < 0.5f)
        {
            hpController.GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("Death");
            unknown.Death();
            monsterSound.PlayOneShot(deathSound);
            state = MonsterState.Death;
        }
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
    private IEnumerator UpdateHpBars(float targetFillAmount)
    {
        redHpBar.fillAmount = targetFillAmount;

        float initialFill = yellowHpBar.fillAmount;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yellowHpBar.fillAmount = Mathf.Lerp(initialFill, targetFillAmount, elapsed / duration);
            yield return null;
        }
        yellowHpBar.fillAmount = targetFillAmount;
    }
}
