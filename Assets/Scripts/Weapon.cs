using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage = 2;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HpController>() != null)
        {
            Debug.Log("Hit " + collision.gameObject.name);
            collision.collider.GetComponent<HpController>().GetDamage(Damage);
        }
    }
}
