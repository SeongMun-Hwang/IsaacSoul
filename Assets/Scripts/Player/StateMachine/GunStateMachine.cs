using UnityEngine;

public class GunStateMachine : StateMachine
{
    StateMachineController player;

    public GunStateMachine(StateMachineController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Gun");
        player.state = State.Idle;
    }
    public void TransitionToAttack()
    {
        player.playerAnimator.SetTrigger("Attack");
        player.state = State.GunAttack;
    }
    public void TransitionToReloading()
    {
        player.state = State.Reload;
        player.playerAnimator.SetTrigger("Reload");
    }
    public void TransitionToDeath()
    {
        player.state = State.Death;
        player.GetComponent<Animator>().SetTrigger("Death");
    }
}
