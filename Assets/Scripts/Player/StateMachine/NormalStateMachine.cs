using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class NormalStateMachine : StateMachine
{
    public PlayerState CurrentState { get; private set; }
    StateMachineController player;

    public DeathState deathState;
    public AttackState attackState; //null
    public NormalStateMachine(StateMachineController player)
    {
        this.player = player;
        deathState=new DeathState(player);
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
}
