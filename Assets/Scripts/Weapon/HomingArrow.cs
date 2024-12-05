using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HomingArrow : MonoBehaviour
{
    public float Damage;
    public string targetTagName;
    public Vector2 Velocity;
    public float homingSpeed = 50f;

    private void FixedUpdate()
    {
        GameObject go = GameObject.FindWithTag(targetTagName);
        if(go != null)
        {
            Vector3 targetPosition = go.transform.position;

            Vector2 direction = (targetPosition - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float currentAngle = transform.eulerAngles.z;

            float angularStep = homingSpeed * Time.deltaTime;
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, angularStep);

            transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
            transform.Translate(Velocity * Time.deltaTime);
        }
        else
        {
            Destroy(go);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HpController>() != null && collision.gameObject.tag == targetTagName)
        {
            collision.gameObject.GetComponent<HpController>().GetDamage(Damage);
        }
        Destroy(gameObject);
    }
}
