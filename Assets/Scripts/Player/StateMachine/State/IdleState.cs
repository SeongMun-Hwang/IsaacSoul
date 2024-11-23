using UnityEngine;

public class IdleState : PlayerState
{
    StateMachineController player;
    public IdleState(StateMachineController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Idle");
    }
    public void Exit()
    {

    }
}