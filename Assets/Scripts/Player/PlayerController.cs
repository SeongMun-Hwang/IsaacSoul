using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Physics
    Rigidbody2D playerRb;
    //Animation
    Animator playerAnimator;

    void Start()
    {
        //Physics
        playerRb = GetComponent<Rigidbody2D>();
        //Animation
        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //Player move
        Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Debug.Log(moveVector);
        playerRb.linearVelocity = moveVector.normalized*PlayerStat.Instance.walkSpeed;
        playerAnimator.SetFloat("InputX", moveVector.x);
        playerAnimator.SetFloat("InputY", moveVector.y);
    }
}
