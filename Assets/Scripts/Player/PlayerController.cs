using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public HpController hpController;
    StaminaController staminaController;

    public Image hpBar;
    public Image yellowHpBar;
    public Image staminaBar;

    public float maxHp = 10;

    public ShortRangeWeapon playerSpear;
    public StateMachineController stateMachineController;

    private void Start()
    {
        staminaController = GetComponent<StaminaController>();
    }
    private void Update()
    {
        //hpBar.fillAmount = hpController.hp / maxHp;
        staminaBar.fillAmount = staminaController.stamina / staminaController.maxStamina;
        float currentHpPercentage = hpController.hp / maxHp;
        StartCoroutine(UpdateHpBars(currentHpPercentage));
    }
    private IEnumerator UpdateHpBars(float targetFillAmount)
    {
        hpBar.fillAmount = targetFillAmount;

        float initialFill = yellowHpBar.fillAmount;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yellowHpBar.fillAmount = Mathf.Lerp(initialFill, targetFillAmount, elapsed / duration);
            yield return null;
        }
        yellowHpBar.fillAmount = targetFillAmount;
    }
}
