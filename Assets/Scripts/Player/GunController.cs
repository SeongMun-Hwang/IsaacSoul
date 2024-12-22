using UnityEngine;

public class GunController : MonoBehaviour
{
    public int currentBullet = 8;
    public int remainBullet = 20;
    public float bulletDamage = 30;
    int maxBullet = 8;
    int addedBullet;

    public GameObject BulletPrefab;
    public GameObject FirePosition;

    public void ShootBullet(float attackAngle)
    {
        Vector3 fireVectorPos = new Vector2();
        if (currentBullet > 0)
        {
            if (attackAngle == 0)//right
            {
                fireVectorPos = new Vector3(0.7f, 0.6f);
            }
            else if (attackAngle == 90)//up
            {
                fireVectorPos = new Vector3(0.35f, 1.5f);
            }
            else if (attackAngle == 180)//left
            {
                fireVectorPos = new Vector3(-0.7f, 0.75f);
            }
            else if (attackAngle == -90)//down
            {
                fireVectorPos = new Vector3(-0.35f, 0.15f);
            }
            fireVectorPos += transform.position;
            GameObject go = Instantiate(BulletPrefab, fireVectorPos, Quaternion.identity);
            go.GetComponent<LongRangeWeapon>().Damage = bulletDamage;
            go.transform.rotation = Quaternion.Euler(0f, 0f, attackAngle);
            currentBullet--;
            PlayerInfo.Instance.playerAudio.PlayGunSound();
        }
    }
    public void ReloadBullet()
    {
        if (remainBullet > 0)
        {
            addedBullet = maxBullet - currentBullet;
            if (addedBullet > remainBullet) addedBullet = remainBullet;
            currentBullet += addedBullet;
            remainBullet -= addedBullet;
        }
    }
}
