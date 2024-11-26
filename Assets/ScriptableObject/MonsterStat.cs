using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStat", menuName = "Scriptable Objects/MonsterStat")]
public class MonsterStat : ScriptableObject
{
    public GameObject monsterPrefab;
    public float moveSpeed;
    //공격
    public float attackRange;
    public float attackDamage;
    public float attackDelay;
    public float attackTimer = 0f;

    //공격 종류
    public int attackVarious;

    public int hp;
}
