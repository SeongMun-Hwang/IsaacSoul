using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static MonsterAgent;

public class BlackSkeleton : MonsterAgent
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
    protected override void Update()
    {
        base.Update();
        if (hpController.hp <= 0 && state != MonsterState.Death&& animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            state = MonsterState.Death;
            animator.SetTrigger("Death");
        }
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
        if (hpController.hp <= 0) return;
        if (CheckAttackDelay())
        {
            if (distanceToTarget.magnitude < attackRange)
            {
                attackTimer = 0f;
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
    private IEnumerator PerformTeleport()
    {
        if(hpController.hp<=0) yield break;
        animator.SetTrigger("Idle");
        state = MonsterState.Idle;

        yield return new WaitForSeconds(0.5f);

        NavMeshHit hit;
        Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y) + direction.normalized * 3f;
        if (NavMesh.SamplePosition(targetPosition, out hit, 1f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        agent.speed = moveSpeed;
        state = MonsterState.Move;
        attackTimer = 1.0f;
    }
}
