using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public enum State
{
    Idle,
    Move,
    SpearAttack,
    GunAttack,
    Reload,
    Death,
}
public class StateMachineController : MonoBehaviour
{
    //StateMachine for each equipment
    public List<StateMachine> stateMachines;
    public SpearStateMachine spearStateMachine;
    public GunStateMachine gunStateMachine;
    public int stateIndex = 0;
    //playerMove
    public Rigidbody2D playerRb;
    public Animator playerAnimator;
    bool isRunPressed = false;
    float moveAngle = 0f;
    public float attackAngle = 0f;
    public float moveSpeed;
    Vector2 moveVector;
    Vector2 attackVector;
    //player input
    InputActionAsset inputActions;
    InputAction moveInput;
    InputAction attackInput;
    //state enum
    public State state;
    //hp
    public HpController hpController;
    float invincibleTime = 1f;
    //stamina
    public StaminaController staminaController;
    //portion
    public int numOfPortion = 2;
    //playercontroller
    public PlayerUI playerController;
    private void Awake()
    {
        stateMachines = new List<StateMachine>();
        spearStateMachine = new SpearStateMachine(this);
        gunStateMachine = new GunStateMachine(this);

        stateMachines.Add(spearStateMachine);
        stateMachines.Add(gunStateMachine);
    }
    private void Start()
    {
        state = State.Idle;
        //player
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        //playerinput
        inputActions = GetComponent<PlayerInput>().actions;
        moveInput = inputActions.FindAction("Move");
        attackInput = inputActions.FindAction("Attack");
        //hp action subscribe
        hpController.OnHpChanged += ActionOnDamage;
        //stamina
        staminaController = GetComponent<StaminaController>();
    }
    private void Update()
    {
        HandleAnimation();
    }
    void HandleAnimation()
    {
        switch (state)
        {
            case State.Idle:
                if (moveSpeed > 6)
                {
                    PlayerInfo.Instance.playerAudio.PlayRunSound();
                }
                else if (moveSpeed > 0 && moveSpeed <= 6)
                {
                    PlayerInfo.Instance.playerAudio.PlayWalkSound();
                }
                else if (moveSpeed == 0)
                {
                    PlayerInfo.Instance.playerAudio.StopSound();
                }
                if (attackVector != Vector2.zero)
                {
                    stateMachines[stateIndex].TransitionToAttack();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (stateMachines[stateIndex] is GunStateMachine)
                    {
                        (stateMachines[stateIndex] as GunStateMachine).TransitionToReloading();
                    }
                }
                if (Input.GetKey(KeyCode.LeftShift) && staminaController.stamina > 0 && (state != State.GunAttack || state != State.Reload))
                {
                    isRunPressed = true;
                }
                else isRunPressed = false;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    stateIndex++;
                    if (stateIndex >= stateMachines.Count)
                    {
                        stateIndex = 0;
                    }
                    stateMachines[stateIndex].Enter();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1) && numOfPortion > 0 && hpController.hp < playerController.maxHp)
                {
                    PlayerInfo.Instance.playerAudio.PlayDrinkSound();
                    hpController.hp += 40;
                    if (hpController.hp > playerController.maxHp)
                    {
                        hpController.hp = playerController.maxHp;
                    }
                    numOfPortion--;
                }
                break;
            case State.SpearAttack:
                playerRb.linearVelocity = Vector2.zero;
                //���� �� ���¹̳��� 0���ϸ�
                if (staminaController.stamina + 10 <= 0)
                {
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f)
                    {
                        stateMachines[stateIndex].Enter();
                        playerAnimator.SetFloat("MoveDirection", attackAngle);
                    }
                }
                //���¹̳� 0�� ������ ���� ����
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {

                    stateMachines[stateIndex].Enter();
                    playerAnimator.SetFloat("MoveDirection", attackAngle);
                }
                break;
            case State.GunAttack:
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    playerAnimator.SetFloat("MoveDirection", attackAngle);
                    stateMachines[stateIndex].Enter();
                }
                break;
            case State.Reload:
                isRunPressed = false;
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    stateMachines[stateIndex].Enter();
                }
                break;
            case State.Death:
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
                    && playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("death"))
                {
                    Destroy(gameObject);
                }
                return;
        }
    }
    private void FixedUpdate()
    {
        PlayerMove();
        PlayerAttack();
    }
    public void PlayerMove()
    {
        //stop move while spear attack
        if (state == State.SpearAttack || state == State.Death) return;
        if (state == State.GunAttack && staminaController.stamina <= 0)
        {
            playerRb.linearVelocity = Vector2.zero;
            moveSpeed = 0;
            return;
        }

        moveVector = moveInput.ReadValue<Vector2>();
        //Player move
        if (moveVector != Vector2.zero)
        {
            if (isRunPressed)
            {
                moveSpeed = PlayerStat.Instance.runSpeed;
            }
            else
            {
                moveSpeed = PlayerStat.Instance.walkSpeed;
            }
            //PlayerIdle
            moveAngle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
            playerAnimator.SetFloat("MoveDirection", moveAngle);
        }
        else
        {
            moveSpeed = 0f;
        }
        //lower moveSpeed while Shooting
        if (state == State.GunAttack)
        {
            moveSpeed /= 2;
        }
        //Normal Equip move speed bonus;
        if (stateMachines[stateIndex] is NormalStateMachine && moveSpeed > 0)
        {
            moveSpeed += 1f;
        }
        playerAnimator.SetFloat("MoveSpeed", moveSpeed);
        playerRb.linearVelocity = moveVector.normalized * moveSpeed;
    }
    void PlayerAttack()
    {
        attackVector = attackInput.ReadValue<Vector2>();
        if (attackVector != Vector2.zero && state != State.SpearAttack)
        {
            attackAngle = Mathf.Atan2(attackVector.y, attackVector.x) * Mathf.Rad2Deg;
            playerAnimator.SetFloat("AttackDirection", attackAngle);
        }
    }
    void ActionOnDamage()
    {
        if (hpController.hp < 0.1)
        {
            moveSpeed = 0f;
            playerAnimator.SetFloat("MoveSpeed", moveSpeed);
            playerRb.linearVelocity = Vector3.zero;
            hpController.enabled = false;
            stateMachines[stateIndex].TransitionToDeath();
        }
        else
        {
            StartCoroutine(GetDamage());
        }
    }
    IEnumerator GetDamage()
    {
        PlayerInfo.Instance.playerAudio.PlayHurtSound();
        hpController.enabled = false;
        for (float f = 0f; f < invincibleTime; f += 0.1f)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            if (renderer.enabled)
            {
                renderer.enabled = false;
            }
            else
            {
                renderer.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        hpController.enabled = true;
    }
    public void ActionOnDeath()
    {
        Destroy(gameObject);
    }
}