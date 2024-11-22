using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class NormalStateMachine : StateMachine
{
    public PlayerState CurrentState { get; private set; }
    StateMachineController player;

    public DeathState deathState;
    public IdleState idleState;
    public MoveState moveState;
    public AttackState attackState; //null
    public NormalStateMachine(StateMachineController player)
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
    public void TransitionToAttack()
    {
        if (attackState != null)
        {
            TransitionTo(attackState);
        }
    }
    public void TransitionToIdle()
    {
        if (idleState != null)
        {
            player.state = State.Idle;
            TransitionTo(idleState);
        }
    }
}
