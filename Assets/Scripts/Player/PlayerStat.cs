using UnityEngine;

public class PlayerStat
{
    private static PlayerStat instance;
    public static PlayerStat Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerStat();
            }
            return instance;
        }
    }

    public float spearAtk = 2f;
    public float gunAtk = 3f;

    public float normalSpeed = 7.5f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
}
