using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    //StateMachine for each equipment
    public NormalStateMachine normalStateMachine;
    public SpearStateMachine spearStateMachine;
    public GunStateMachine gunStateMachine;
    //StateMachine List for change equipment
    public List<StateMachine> stateMachines;
    public int stateMachineIndex;

    //Physics
    Rigidbody2D playerRb;
    //Animation
    Animator playerAnimator;
    //bool var for run
    bool isRunPressed = false;
    //AnimationState
    float angle = 0f;

    enum EquipState
    {
        Normal,
        Gun,
        Run,
    }
    EquipState equipState;

    private void Awake()
    {
        //StateMachien Initialize
        stateMachines = new List<StateMachine>();
        normalStateMachine = new NormalStateMachine();
        spearStateMachine = new SpearStateMachine();
        gunStateMachine = new GunStateMachine();      
    }
    void Start()
    {
        //stateMachine
        stateMachines.Add(normalStateMachine);
        stateMachines.Add(spearStateMachine);
        stateMachines.Add(gunStateMachine);

        stateMachineIndex = 0;
        //Physics
        playerRb = GetComponent<Rigidbody2D>();
        //Animation
        playerAnimator = GetComponent<Animator>();
        //enum state
        equipState = EquipState.Normal;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunPressed = true;
        }
        else isRunPressed = false;
    }
    private void FixedUpdate()
    {
        //Player move
        Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveVector.sqrMagnitude > 0)
        {
            if (isRunPressed)
            {
                //playerRb.linearVelocity = moveVector.normalized * PlayerStat.Instance.runSpeed;
                playerAnimator.SetFloat("InputX", moveVector.x * 2);
                playerAnimator.SetFloat("InputY", moveVector.y * 2);
            }
            else
            {
                //playerRb.linearVelocity = moveVector.normalized * PlayerStat.Instance.walkSpeed;
                playerAnimator.SetFloat("InputX", moveVector.x);
                playerAnimator.SetFloat("InputY", moveVector.y);
            }
            //PlayerIdle
            angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
            Debug.Log(angle / 90);
        }
        playerAnimator.SetFloat("Direction", angle / 90);
    }
}
