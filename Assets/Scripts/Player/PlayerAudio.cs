using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource playerAudio;

    //ȿ���� ���
    [SerializeField] AudioClip gunSound;
    [SerializeField] AudioClip spearSound;
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip runSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip HurtSound;
    [SerializeField] AudioClip DrinkSound;

    private string currentSound = "";

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }
    public void PlayRunSound()
    {
        if (currentSound != "run")
        {
            playerAudio.Stop();
            playerAudio.PlayOneShot(runSound);
            currentSound = "run";
        }
    }
    public void PlayWalkSound()
    {
        if (currentSound != "walk")
        {
            playerAudio.Stop();
            playerAudio.PlayOneShot(walkSound);
            currentSound = "walk";
        }
    }
    public void StopSound()
    {
        if (currentSound != "none")
        {
            playerAudio.Stop();
            currentSound = "none";
        }
    }
    public void PlayDrinkSound()
    {
        playerAudio.Stop();
        playerAudio.PlayOneShot(DrinkSound);
    }
    public void PlayHurtSound()
    {
        playerAudio.Stop();
        playerAudio.PlayOneShot(HurtSound);
    }
    public void PlayGunSound()
    {
        playerAudio.Stop();
        playerAudio.PlayOneShot(gunSound);
    }
    public void PlayReloadSound()
    {
        playerAudio.Stop();
        playerAudio.PlayOneShot(reloadSound);
    }    public void PlaySpearSound()
    {
        playerAudio.Stop();
        playerAudio.PlayOneShot(spearSound);
    }
}
