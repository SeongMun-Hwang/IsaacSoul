using UnityEngine;
using UnityEngine.AI;

public class LaserController : MonoBehaviour
{
    Animator laserAnimator;
    GameObject player;
    public GameObject laserRotationPoint;
    public NavMeshAgent stoneGolemAgent;

    public float rotationSpeed = 360f;
    private float rotationTime = 0f;
    private float totalRotationDuration = 0.5f;

    public AudioSource golemAudioSource;
    void Start()
    {
        laserAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            RotateLaser();
        }
    }
    public void RotateLaser()
    {
        Vector3 targetDirection = (player.transform.position - laserRotationPoint.transform.position).normalized;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        if (rotationTime < totalRotationDuration)
        {
            laserRotationPoint.transform.rotation = Quaternion.RotateTowards(
                laserRotationPoint.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rotationTime += Time.deltaTime;
        }
        else
        {
            rotationTime = 0f;
        }
    }
    public void EndLaser()
    {
        Debug.Log("EndLaser");
        laserAnimator.GetComponent<Animator>().SetTrigger("Idle");
        stoneGolemAgent.isStopped = false;
        golemAudioSource.Stop();
    }
}