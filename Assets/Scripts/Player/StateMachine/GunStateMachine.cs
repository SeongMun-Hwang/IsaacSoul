using UnityEngine;

public class GunStateMachine : StateMachine
{
    StateMachineController player;
    int maxBullet = 8;
    int addedBullet;
    public GunStateMachine(StateMachineController player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.GetComponent<Animator>().SetTrigger("Gun");
        player.state = State.Idle;
    }
    public void TransitionToAttack()
    {
        player.playerAnimator.SetTrigger("Attack");
        player.ShootBullet();
        player.state = State.GunAttack;
    }
    public void TransitionToReloading()
    {
        addedBullet = maxBullet - player.currentBullet;
        player.currentBullet += addedBullet;
        player.totalBullet -= addedBullet;
        player.state = State.Reload;
        player.playerAudio.PlayOneShot(player.reloadSound);
        player.playerAnimator.SetTrigger("Reload");
    }
    public void TransitionToDeath()
    {
        player.state = State.Death;
        player.GetComponent<Animator>().SetTrigger("Death");
    }
}
