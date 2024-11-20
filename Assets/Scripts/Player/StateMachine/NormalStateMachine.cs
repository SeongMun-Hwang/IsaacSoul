using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class NormalStateMachine : StateMachine
{
    public PlayerState CurrentState { get; private set; }
    StateController player;

    public DeathState deathState;
    public IdleState idleState;
    public MoveState moveState;

    public NormalStateMachine(StateController player)
    {
        this.player = player;
        deathState=new DeathState(player);
        idleState=new IdleState(player);
        moveState=new MoveState(player);
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Normal");
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
}
