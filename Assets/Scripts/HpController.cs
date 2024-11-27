using System;
using UnityEngine;

public class HpController : MonoBehaviour
{
    public int hp;
    public event Action OnHpChanged;

    public void GetDamage(int damage)
    {
        if (!enabled)
        {
            return;
        }
        hp -= damage;
        OnHpChanged?.Invoke();
    }
}