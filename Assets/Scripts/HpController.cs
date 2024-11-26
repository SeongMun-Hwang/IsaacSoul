using System;
using UnityEngine;

public class HpController : MonoBehaviour
{
    public int hp;
    public event Action OnHpChanged;

    Animator animator;
    CapsuleCollider2D capsuleCollider;
    public void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    public void GetDamage(int damage)
    {
        hp -= damage;
        OnHpChanged?.Invoke();
    }
}