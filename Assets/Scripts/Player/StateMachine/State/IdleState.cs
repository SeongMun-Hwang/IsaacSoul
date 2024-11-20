using UnityEngine;

public class IdleState : PlayerState
{
    StateController player;
    public IdleState(StateController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Idle");
    }
}