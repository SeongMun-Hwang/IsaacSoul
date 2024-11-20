using UnityEngine;

public class PlayerStat
{
    private static PlayerStat instance;
    public static PlayerStat Instance {  
        get
        {
            if(instance == null)
            {
                instance = new PlayerStat();
            }
            return instance;
        }
    }
    
    public float spearAtk = 2;
    public float gunAtk = 3;

    public float walkSpeed = 5;
    public float runSpeed = 10;
}
