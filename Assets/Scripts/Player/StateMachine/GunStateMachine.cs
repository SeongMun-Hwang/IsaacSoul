using UnityEngine;

public class GunStateMachine : StateMachine
{
    public PlayerState CurrentState { get; private set; }
    StateMachineController player;

    public DeathState deathState;
    public AttackState attackState;
    public ReloadingState reloadingState;

    public GunStateMachine(StateMachineController player)
    {
        this.player = player;
        deathState = new DeathState(player);
        attackState = new AttackState(player);
        reloadingState = new ReloadingState(player);
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Gun");
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
            player.state = State.GunAttack;
            TransitionTo(attackState);
        }
    }
    public void TransitionToReloading()
    {
        player.state = State.Reload;
        TransitionTo(reloadingState);
    }
    public void TransitionToDeath()
    {
        Enter();
        player.state = State.Death;
        TransitionTo(deathState);
    }
}
