using System.Collections;
using UnityEngine;

public class CanvasAnimator : MonoBehaviour
{
    public LongRangeWeapon bullet;
    float timer = 0;
    public AudioSource audioSource;
    bool animationPlay = false;
    public GameObject player;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f && !animationPlay)
        {
            animationPlay = true;
            audioSource.Play();
            bullet.Velocity = new Vector2(1000, 0);
            StartCoroutine(PlayerApear());
        }
    }
    IEnumerator PlayerApear()
    {
        yield return new WaitForSeconds(3f);
        player.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-750, 0);
        yield return new WaitForSeconds(2.5f);
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}
