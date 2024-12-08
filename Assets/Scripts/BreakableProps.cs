using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BreakableProps : MonoBehaviour
{
    public SpriteRenderer currentImg;
    public Sprite brokenImg;
    public HpController hpController;
    float maxHp;

    private void Start()
    {
        hpController = GetComponent<HpController>();
        maxHp = hpController.hp;
    }
    void Update()
    {
        if (hpController.hp < maxHp / 2 && brokenImg != null)
        {
            currentImg.sprite = brokenImg;
        }
        if (hpController.hp <= 1f)
        {
            Destroy(gameObject);
        }
    }
}