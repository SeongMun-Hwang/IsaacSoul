using UnityEngine;

public class ImageUnknown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Death");
        Destroy(collision.gameObject);
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
