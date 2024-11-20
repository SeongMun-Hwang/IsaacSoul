using UnityEngine;

public class MoveState : PlayerState
{
    StateController player;
    public MoveState(StateController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Move");
    }
}