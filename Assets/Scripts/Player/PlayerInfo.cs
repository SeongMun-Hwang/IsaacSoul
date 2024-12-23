using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private static PlayerInfo instance;
    public static PlayerInfo Instance { get { return instance; } }

    public GameObject player;

    public PlayerAudio playerAudio;
    public GunController gunController;
    public HpController hpController;
    void Awake()
    {
        //if (instance != null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerRoot");
    }

    void Update()
    {
        
    }
}
