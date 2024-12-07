using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Animator animator;
    public float damage = 20;
    private void Start()
    {
        animator.SetTrigger("Cast");
    }
    public void Explode()
    {
        animator.SetTrigger("Explode");
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HpController hpController = collision.transform.GetComponentInChildren<HpController>();
        if (hpController != null && hpController.transform.CompareTag("Player"))
        {
            collision.transform.GetComponentInChildren<HpController>().GetDamage(damage);
        }
    }
}