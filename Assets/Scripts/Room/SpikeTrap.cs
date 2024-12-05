using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    Animator animator;
    float activeTimer = 2f;
    float currentTime = 0f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= activeTimer)
        {
            animator.SetTrigger("Active");
            currentTime = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HpController>() != null)
        {
            collision.GetComponent<HpController>().GetDamage(10);
        }
    }
}
