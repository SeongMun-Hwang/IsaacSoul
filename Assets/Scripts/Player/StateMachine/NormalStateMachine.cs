using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class NormalStateMachine : StateMachine
{
    StateMachineController player;

    public NormalStateMachine(StateMachineController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Normal");
        player.state = State.Idle;
    }
    public void TransitionToAttack()
    {
        
    }
    public void TransitionToDeath()
    {
        Enter();
        player.state = State.Death;
        player.playerAnimator.SetTrigger("Death");
    }
}
