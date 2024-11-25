using UnityEngine;

public class AttackState : PlayerState
{
    StateMachineController player;
    public AttackState(StateMachineController player)
    {
        this.player = player;
    }
    public void Enter()
    {        
        player.GetComponent<Animator>().SetTrigger("Attack");
    }
    public void Exit()
    {

    }
}