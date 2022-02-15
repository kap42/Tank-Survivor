using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMineLauncher : MonoBehaviour
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
            float shotAngle =
                (-bulletSpread / 2f) +
                ((bulletSpread / 2f) / bullets);

            shotAngle *= Random.Range(-.85f, 1.25f);

            for (int i = 0; i < bullets; i++)
            {
                Vector2 shotDir =
                    Quaternion.Euler(0, 0, shotAngle) *
                    transform.up *
                    -2;

                GameObject go = Instantiate(
                    bullet,
                    (Vector2)transform.position + shotDir,
                    transform.rotation);

                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                rb.velocity = bulletSpeed * shotDir;
                rb.AddTorque(Random.Range(-1f,1f));


                shotAngle += (bulletSpread / bullets);
                shotAngle *= Random.Range(-.85f, 1.25f);
            }

            yield return new WaitForSeconds(bulletCoolDownTime);
        }

    }
}
