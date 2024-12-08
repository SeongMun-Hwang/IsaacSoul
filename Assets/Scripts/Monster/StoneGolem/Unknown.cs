using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unknown : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject floorAttackPrefab;
    public List<Func<IEnumerator>> attackVarious;
    float attackDuration = 5f;
    public float attackTimer = 0;

    public float floorAttackNum = 20;
    public bool isAttacking = false;

    public Animator unknownAnimator;
    void Start()
    {
        attackVarious = new List<Func<IEnumerator>>();
        attackVarious.Add(FloorAttack);
        attackVarious.Add(TripleAttack);
    }

    private void FixedUpdate()
    {
        attackTimer += Time.fixedDeltaTime;
        if (attackDuration <= attackTimer && !isAttacking)
        {
            int rand = UnityEngine.Random.Range(0, attackVarious.Count);
            StartCoroutine(attackVarious[rand]());
        }
    }
    public IEnumerator ShootInCircle()
    {
        isAttacking = true;
        float attackAngle = 0;
        float shootingTime = 3f;
        float circleDuration = 0.05f;
        float timer = 0f;
        while (timer <= shootingTime)
        {
            GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            go.transform.rotation = Quaternion.Euler(0f, 0f, attackAngle);
            timer += circleDuration;
            attackAngle += circleDuration * 500;
            yield return new WaitForSeconds(circleDuration);
        }
        attackTimer = 0;
        isAttacking = false;
    }
    public IEnumerator FloorAttack()
    {
        unknownAnimator.SetTrigger("FloorAttack");
        isAttacking = true;
        for (int i = 0; i < floorAttackNum; i++)
        {
            Vector3 instantiatePos = transform.root.position + new Vector3(UnityEngine.Random.Range(15, -15), UnityEngine.Random.Range(-9, 9));
            GameObject go = Instantiate(floorAttackPrefab, instantiatePos, Quaternion.identity);
        }
        yield return null;
        attackTimer = 0;
        isAttacking = false;
        unknownAnimator.SetTrigger("Idle");
    }
    public IEnumerator TripleAttack()
    {
        isAttacking = true;
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 attackDirection = (player.transform.position - transform.position).normalized;
        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        float angleDiff = 15f;
        GameObject go1 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        go1.transform.rotation = Quaternion.Euler(0f, 0f, attackAngle);
        GameObject go2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        go2.transform.rotation = Quaternion.Euler(0f, 0f, attackAngle - angleDiff);
        GameObject go3 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        go3.transform.rotation = Quaternion.Euler(0f, 0f, attackAngle + angleDiff);
        yield return null;
        attackTimer = 0;
        isAttacking = false;
    }
    public void Death()
    {
        unknownAnimator.SetTrigger("Death");
    }
}
