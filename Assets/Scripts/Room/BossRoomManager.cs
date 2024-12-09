using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    public GameObject boss;
    public GameObject winCanvas;
    private void Update()
    {
        if(boss == null)
        {
            winCanvas.SetActive(true);
        }
    }
}
