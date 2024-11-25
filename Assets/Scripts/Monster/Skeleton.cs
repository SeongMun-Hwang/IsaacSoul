using UnityEngine;

public class Skeleton : MonsterAgent
{
    float attackRange = 2f;
    float moveSpeed = 3.5f;
    MonsterState state;
    private void Start()
    {
        state = MonsterState.Move;
    }
    private void Update()
    {
        base.Update();
        switch (state)
        {
            case MonsterState.Idle:

                break;
            case MonsterState.Move:
                if (base.distanceToTarget.magnitude < attackRange)
                {
                    int rand = Random.Range(0, 2);
                    base.animator.SetFloat("AttackType", (float)rand);
                    base.animator.SetTrigger("Attack");
                    state = MonsterState.Attack;
                }
                break;
            case MonsterState.Attack:
                base.agent.speed = 0f;
                if (base.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    base.animator.SetTrigger("Idle");
                    base.agent.speed = moveSpeed;
                    state = MonsterState.Move;
                }
                break;
        }
    }
}