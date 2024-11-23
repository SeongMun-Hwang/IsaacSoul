using UnityEngine;

public class MoveState : PlayerState
{
    StateMachineController player;
    public MoveState(StateMachineController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Move");
    }
    public void Exit()
    {

    }
}