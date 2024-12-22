using UnityEngine;

public class SpearStateMachine : StateMachine
{
    StateMachineController player;

    public SpearStateMachine(StateMachineController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Spear");
        player.state = State.Idle;
    }
    public void TransitionToAttack()
    {
        player.state = State.SpearAttack;
        player.staminaController.stamina -= 10f;
        PlayerInfo.Instance.playerAudio.PlaySpearSound();
        player.GetComponent<Animator>().SetTrigger("Attack");
    }
    public void TransitionToDeath()
    {
        Enter();
        player.state = State.Death;
        player.GetComponent<Animator>().SetTrigger("Death");
    }
}
