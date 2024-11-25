using UnityEngine;

public class Spear : MonoBehaviour
{
    public int spearDamage=2;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<HpController>() != null)
        {
            collision.collider.GetComponent<HpController>().GetDamage(spearDamage);
        }
    }
}
