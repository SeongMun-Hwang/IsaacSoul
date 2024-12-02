using Unity.Cinemachine;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject connectedDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = connectedDoor.transform.position;

        gameObject.transform.root.gameObject.SetActive(false);
        connectedDoor.transform.root.gameObject.SetActive(true);
    }
}