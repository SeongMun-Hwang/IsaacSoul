using UnityEngine;

public class ShortRangeWeapon : MonoBehaviour
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
}
