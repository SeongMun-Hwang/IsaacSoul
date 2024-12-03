using System;
using System.Collections;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    StateMachineController stateMachineController;
    float moveSpeed;
    State state;

    public float stamina;
    public float maxStamina = 100;

    public float staminaRestoreSpeed = 5f;
    public float staminaCosumeSpeed = 10f;

    bool isCoolDown = false;
    float staminaRestoreCooltime = 2f;


    void Start()
    {
        stamina = maxStamina;
        stateMachineController = GetComponent<StateMachineController>();
    }

    void Update()
    {
        ControlStaminaByState();
    }
    void ControlStaminaByState()
    {
        state = stateMachineController.state;
        moveSpeed = stateMachineController.moveSpeed;
        switch (state)
        {
            case State.Idle:
                //빈손이면 빠른 회복
                if (stateMachineController.stateMachines[stateMachineController.stateIndex] is NormalStateMachine)
                {
                    staminaRestoreSpeed = 12f;
                }
                else
                {
                    staminaRestoreSpeed = 10f;
                }
                if (moveSpeed > 6 && stamina > 0)
                {
                    stamina -= staminaCosumeSpeed * Time.deltaTime;
                }
                else if (!isCoolDown)
                {
                    RestoreStamina();
                }
                break;
            case State.GunAttack:
                if (moveSpeed > 0)
                {
                    stamina -= staminaCosumeSpeed * Time.deltaTime;
                }
                break;
            case State.Death:
                while (stamina >= 0)
                {
                    stamina -= staminaCosumeSpeed * Time.deltaTime;
                }
                break;
        }
        if (stamina <= 0 && !isCoolDown)
        {
            StartCoroutine(StaminaRestoreCoolTime());
        }
    }
    public void RestoreStamina()
    {
        if (stamina < 0)
        {
            stamina = 0f;
        }
        if (!isCoolDown && stamina < maxStamina)
        {
            stamina += staminaRestoreSpeed * Time.deltaTime;
        }
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }
    IEnumerator StaminaRestoreCoolTime()
    {
        isCoolDown = true;
        yield return new WaitForSeconds(staminaRestoreCooltime);
        isCoolDown = false;
    }
}
