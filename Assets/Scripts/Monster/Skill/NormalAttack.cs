using UnityEngine;

public class NormalAttack : MonsterSkill
{
    public AudioClip normalAttackClip;
    public override void Execute(MonsterAgent agent)
    {
        agent.SetAnimationTrigger(triggerStr);
    }
}
