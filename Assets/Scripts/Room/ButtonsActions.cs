using UnityEngine;

public class ButtonsActions : MonoBehaviour
{
    GameObject player;
    HpController hpController;
    StaminaController staminaController;
    PlayerController playerController;
    GameObject RewardCanvas;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hpController=player.GetComponentInChildren<HpController>();
        staminaController=player.GetComponent<StaminaController>();
        playerController = player.GetComponent<PlayerController>();
        RewardCanvas = transform.parent.parent.gameObject;
    }
    public void IncreaseHp(int hp)
    {
        playerController.maxHp += hp;
        hpController.hp += hp;
        ButtonQuit();
    }
    public void RestoreHp(int hp)
    {
        hpController.hp += hp;
        if (hpController.hp > playerController.maxHp)
        {
            hpController.hp = playerController.maxHp;
        }
        ButtonQuit();
    }
    public void IncreaseStamina(float stamina)
    {
        staminaController.maxStamina += stamina;
        ButtonQuit();
    }
    public void IncreaseSpearDamage(float damage)
    {
        playerController.playerSpear.Damage += damage;
        ButtonQuit();
    }
    public void IncreaseBulletDamage(float damage)
    {
        playerController.stateMachineController.bulletDamage += damage;
        ButtonQuit();
    }
    public void IncreaseBulletNumber(int number)
    {
        playerController.stateMachineController.remainBullet += number;
        ButtonQuit();
    }
    public void ButtonQuit()
    {
        RewardCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
