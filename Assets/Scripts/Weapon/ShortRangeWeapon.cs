using UnityEngine;

public class ShortRangeWeapon : MonoBehaviour
{
    public int Damage;
    public string targetTagName;
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HpController>() != null && collision.gameObject.tag == targetTagName)
        {
            collision.collider.GetComponent<HpController>().GetDamage(Damage);
        }
    }
}
