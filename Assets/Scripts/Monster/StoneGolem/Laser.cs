using UnityEngine;

public class Laser : MonoBehaviour
{
    public float Damage;
    public string targetTagName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HpController>() != null && (collision.gameObject.tag == targetTagName))
        {
            collision.gameObject.GetComponent<HpController>().GetDamage(Damage);
        }
    }
}
