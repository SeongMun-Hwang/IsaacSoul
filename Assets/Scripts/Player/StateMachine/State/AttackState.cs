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
        //if (player.stateMachines[player.stateIndex] is GunStateMachine)
        //{
        //    player.moveSpeed /= 2;
        //}
        player.GetComponent<Animator>().SetTrigger("Attack");
    }
    public void Exit()
    {

    }
}