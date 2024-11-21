using UnityEngine;

public class ReloadingState : PlayerState
{
    StateMachineController player;
    public ReloadingState(StateMachineController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Reloading");
    }
    public void Exit()
    {

    }
}