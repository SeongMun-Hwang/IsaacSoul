using UnityEngine;

public class AttackState : PlayerState
{
    StateController player;
    public AttackState(StateController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Attack");
    }
}