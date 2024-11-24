using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum State
{
    Idle,
    SpearAttack,
    GunAttack,
    Move,
    Reload,
    Death,
}
public class StateMachineController : MonoBehaviour
{
    //StateMachine for each equipment
    public List<StateMachine> stateMachines;
    int equipIndex = 0;

    public NormalStateMachine normalStateMachine;
    public SpearStateMachine spearStateMachine;
    public GunStateMachine gunStateMachine;
    int stateIndex = 0;
    //playerMove
    Rigidbody2D playerRb;
    Animator playerAnimator;
    bool isRunPressed = false;
    float moveAngle = 0f;
    float attackAngle = 0f;
    float moveSpeed;
    Vector2 moveVector;
    Vector2 attackVector;
    //player input
    InputActionAsset inputActions;
    InputAction moveInput;
    InputAction attackInput;

    //state enum
    public State state;
    private void Awake()
    {
        stateMachines = new List<StateMachine>();
        normalStateMachine = new NormalStateMachine(this);
        spearStateMachine = new SpearStateMachine(this);
        gunStateMachine = new GunStateMachine(this);

        stateMachines.Add(normalStateMachine);
        stateMachines.Add(spearStateMachine);
        stateMachines.Add(gunStateMachine);
    }
    private void Start()
    {
        stateMachines[equipIndex].Enter();
        state = State.Idle;
        //player
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        //playerinput
        inputActions = GetComponent<PlayerInput>().actions;
        moveInput = inputActions.FindAction("Move");
        attackInput = inputActions.FindAction("Attack");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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
        HandleAnimation();
    }
    void HandleAnimation()
    {
        Debug.Log(state);
        switch (state)
        {
            case State.Idle:
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
                break;
            case State.SpearAttack:
                playerRb.linearVelocity = Vector2.zero;
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    stateMachines[stateIndex].Enter();
                }
                break;
            case State.GunAttack:
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    stateMachines[stateIndex].Enter();
                }
                break;
            case State.Reload:
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    stateMachines[stateIndex].Enter();
                }
                break;
            case State.Move:
                if (moveVector == Vector2.zero)
                {
                    moveSpeed = 0f;
                    stateMachines[stateIndex].Enter();
                }
                if (attackVector != Vector2.zero)
                {
                    stateMachines[stateIndex].TransitionToAttack();
                }
                break;
            case State.Death:

                break;
        }
    }
    private void FixedUpdate()
    {
        PlayerMove();
        PlayerAttack();
    }
    public void PlayerMove()
    {
        if (state == State.SpearAttack || state == State.GunAttack) return;

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
        playerAnimator.SetFloat("MoveSpeed", moveSpeed);
        playerRb.linearVelocity = moveVector.normalized * moveSpeed;
    }
    void PlayerAttack()
    {
        //if (state == State.SpearAttack || state==State.GunAttack) return;

        attackVector = attackInput.ReadValue<Vector2>();
        if (attackVector != Vector2.zero)
        {
            attackAngle = Mathf.Atan2(attackVector.y, attackVector.x) * Mathf.Rad2Deg;
            playerAnimator.SetFloat("AttackDirection", attackAngle);
            playerAnimator.SetFloat("MoveDirection", attackAngle);
        }
    }
}