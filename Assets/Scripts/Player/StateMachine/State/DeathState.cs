using UnityEngine;

public class DeathState : PlayerState
{
    StateController player;
    public DeathState(StateController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Death");
    }
}