using System.Collections;
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
        if (collision.gameObject.CompareTag("PlayerRoot"))
        {
            Vector3 nextRoomPos = connectedDoor.transform.root.gameObject.transform.position;
            StartCoroutine(MoveBorder(nextRoomPos));
            collision.gameObject.transform.position = connectedDoor.transform.position;
        }
    }
    private IEnumerator MoveBorder(Vector3 targetPosition)
    {
        connectedDoor.transform.root.gameObject.SetActive(true);
        float borderSpeed = 30f;
        Vector2 distance = cineBorder.transform.position - targetPosition;
        if (Mathf.Abs(distance.x) > 0)
        {
            borderSpeed = 60f;
        }
        Debug.Log(targetPosition);
        Debug.Log(borderSpeed);
        while (Vector3.Distance(cineBorder.transform.position, targetPosition) > 0.01f)
        {
            cineBorder.transform.position = Vector3.MoveTowards(
                cineBorder.transform.position,
                targetPosition,
                Time.deltaTime * borderSpeed
            );
            yield return null; // 다음 프레임까지 대기
        }
        cineBorder.transform.position = targetPosition;
        gameObject.transform.root.gameObject.SetActive(false);
    }
}