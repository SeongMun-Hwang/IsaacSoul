using UnityEngine;

public class DeathState : PlayerState
{
    StateMachineController player;
    public DeathState(StateMachineController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Death");
    }
    public void Exit()
    {

    }
}