using UnityEngine;

public class HpController : MonoBehaviour
{
    public int hp;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    public void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    public void GetDamage(int attack)
    {
        hp -= attack;
        capsuleCollider.isTrigger = true;
        animator.SetTrigger("Hit");
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            capsuleCollider.isTrigger = false;
            animator.SetTrigger("Idle");
        }
    }
}