using UnityEngine;

public class LancerSpear : MonoBehaviour
{
    public float Damage;
    public string targetTagName;
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HpController>() != null && collision.gameObject.tag == targetTagName)
        {
            collision.gameObject.GetComponent<HpController>().GetDamage(Damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HpController>() != null)
        {
            collision.gameObject.GetComponent<HpController>().GetDamage(Damage);
        }
    }
}
