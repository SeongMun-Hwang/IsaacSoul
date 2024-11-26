using TMPro;
using UnityEngine;

public class Skeleton : MonsterAgent
{
    public TextMeshPro stateText;
    private void Start()
    {
        state = MonsterState.Move;
        attackRange = 2f;
        moveSpeed = 3f;
        agent.speed = moveSpeed;
        attackVarious = 2;
        attackDelay = 2f;

        hpController.OnHpChanged += HandleHpState;
        state = MonsterState.Move;
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
        if (attackTimer < attackDelay)
        {
            attackTimer += Time.deltaTime;
        }
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
            attackTimer = 0f;
            state = MonsterState.Move;
        }
    }
    private void HandleHpState()
    {
        attackTimer = 0f;
        state = MonsterState.Hit;
        animator.SetTrigger("Hit");
    }
    private void HandleHitState()
    {
        agent.speed = 0f;
        hpController.enabled = false;

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (hpController.hp < 1)
            {
                animator.SetTrigger("Death");
                state = MonsterState.Death;
            }
            else
            {
                animator.SetTrigger("Idle");
                hpController.enabled = true;
                agent.speed = moveSpeed;
                state = MonsterState.Move;
            }
        }
    }
    private void HandleDeathState()
    {
        agent.speed = 0f;
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