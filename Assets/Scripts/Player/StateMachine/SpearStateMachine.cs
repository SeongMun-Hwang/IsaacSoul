using UnityEngine;

public class SpearStateMachine : StateMachine
{
    public PlayerState CurrentState { get; private set; }
    StateMachineController player;

    public DeathState deathState;
    public AttackState attackState;

    public SpearStateMachine(StateMachineController player)
    {
        this.player = player;
        deathState = new DeathState(player);
        attackState = new AttackState(player);
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Spear");
        player.state = State.Idle;
    }
    public void Initialize(PlayerState state)
    {
        CurrentState = state;
        state.Enter();
    }
    public void TransitionTo(PlayerState nextState)
    {
        CurrentState = nextState;
        CurrentState.Enter();
    }
    public void TransitionToAttack()
    {
        if (attackState != null)
        {
            player.state = State.SpearAttack;
            //player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            TransitionTo(attackState);
        }
    }
}
