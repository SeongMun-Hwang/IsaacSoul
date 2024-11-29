using System;
using UnityEngine;

public class HpController : MonoBehaviour
{
    public float hp;
    public event Action OnHpChanged;

    public void GetDamage(float damage)
    {
        if (!enabled)
        {
            return;
        }
        hp -= damage;
        OnHpChanged?.Invoke();
    }
}