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
        //PlayerInfo.Instance.gunController.ShootBullet();
        player.state = State.GunAttack;
    }
    public void TransitionToReloading()
    {
        PlayerInfo.Instance.gunController.ReloadBullet();
        player.state = State.Reload;
        PlayerInfo.Instance.playerAudio.PlayReloadSound();
        player.playerAnimator.SetTrigger("Reload");
    }
    public void TransitionToDeath()
    {
        Enter();
        player.state = State.Death;
        player.GetComponent<Animator>().SetTrigger("Death");
    }
}
