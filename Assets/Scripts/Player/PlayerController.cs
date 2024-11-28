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
}
