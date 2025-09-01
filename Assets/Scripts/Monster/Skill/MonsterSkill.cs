using UnityEngine;

public abstract class MonsterSkill : MonoBehaviour
{
    [Tooltip("Skill Cooltime")]
    public float cooldown = 2f;
    [Tooltip("Skill Trigger")]
    public string triggerStr;
    
    public abstract void Execute(MonsterAgent agent);
}
