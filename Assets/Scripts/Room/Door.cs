using Unity.Cinemachine;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject connectedDoor;
    public GameObject cineBorder;
    public GameObject cineCam;

    Vector3 horizontalDistance = new Vector2(34, 0);
    Vector3 verticalDistance = new Vector2(0, 22);
    private void Start()
    {
        cineBorder = GameObject.FindGameObjectWithTag("CineBorder");
        cineCam = GameObject.FindGameObjectWithTag("CineCam");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //cineCam.GetComponent<CinemachineConfiner2D>().enabled = false;
            collision.gameObject.transform.position = connectedDoor.transform.position;
            cineBorder.gameObject.transform.position = connectedDoor.transform.root.gameObject.transform.position;
            cineCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = cineBorder.GetComponent<PolygonCollider2D>();
            //cineCam.GetComponent<CinemachineConfiner2D>().enabled = true;
            //Camera.main.transform.position = connectedDoor.transform.root.gameObject.transform.position;

            gameObject.transform.root.gameObject.SetActive(false);
            connectedDoor.transform.root.gameObject.SetActive(true);
        }
    }
}