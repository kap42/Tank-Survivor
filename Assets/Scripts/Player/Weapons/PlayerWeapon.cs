using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bullet;

    public int bullets = 1;
    public int maxBullets = 10;
    public float bulletSpeed = 10;
    public float bulletCoolDownTime = .1f;
    public float bulletSpread = 45f;

    IEnumerator Start()
    {
        while (true)
        {
            for (int i = 0; i < bullets; i++)
            {
                Instantiate(
                    bullet,
                    transform.position,
                    transform.rotation);
            }

            yield return new WaitForSeconds(bulletCoolDownTime);
        }
    }
}
