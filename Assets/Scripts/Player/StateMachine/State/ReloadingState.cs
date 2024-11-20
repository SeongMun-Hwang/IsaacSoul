using UnityEngine;

public class ReloadingState : PlayerState
{
    StateController player;
    public ReloadingState(StateController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Reloading");
    }
}