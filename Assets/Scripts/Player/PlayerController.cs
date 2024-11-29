using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    HpController hpController;
    StaminaController staminaController;

    public Image hpBar;
    public Image staminaBar;

    float maxHp = 10;
    int maxStamina = 100;

    public ShortRangeWeapon playerSpear;
    public LongRangeWeapon playerBullet;
    public StateMachineController stateMachineController;

    private void Start()
    {
        hpController = GetComponent<HpController>();
        staminaController = GetComponent<StaminaController>();
    }
    private void Update()
    {
        hpBar.fillAmount = hpController.hp / maxHp;
        staminaBar.fillAmount = staminaController.stamina / maxStamina;
    }
    public void IncreaseHp(int hp)
    {
        hpController.hp += hp;
    }
    public void IncreaseStamina(float stamina)
    {
        staminaController.maxStamina += stamina;
    }
    public void IncreaseSpearDamage(float damage)
    {
        playerSpear.Damage += damage;
    }   
    public void IncreaseBulletDamage(float damage)
    {
        playerBullet.Damage += damage;
    }
    public void IncreaseBulletNumber(int number)
    {
        stateMachineController.totalBullet += number;
    }
}
