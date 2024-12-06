using UnityEngine;

public class LongRangeWeapon : MonoBehaviour
{
    public float Damage;
    public string targetTagName;
    public Vector2 Velocity;
    private void FixedUpdate()
    {
        transform.Translate(Velocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HpController>() != null && collision.gameObject.tag == targetTagName)
        {
            collision.gameObject.GetComponent<HpController>().GetDamage(Damage);
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HpController>() != null && (collision.gameObject.tag == targetTagName))
        {
            collision.gameObject.GetComponent<HpController>().GetDamage(Damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
