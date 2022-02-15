using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    /// <summary>
    /// The Game Object that is to be used as a bullet
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// Number of bullets currently fired
    /// </summary>
    public int bullets = 1;

    /// <summary>
    /// Spread the bullets within this arc
    /// </summary>
    public float bulletSpread = 45f;

    /// <summary>
    /// The movement speed of the bullets
    /// </summary>
    public float bulletSpeed = 10;

    /// <summary>
    /// How long to wait between firing the bullets
    /// </summary>
    public float bulletCoolDownTime = .1f;

    /// <summary>
    /// How long should the bullets remain in the scene
    /// </summary>
    public float bulletLifeTime = 2f;

    IEnumerator Start()
    {
        // Fail if the bullet isn't set
        if (bullet == null)
        {
            Debug.LogError("Bullet isn't set to an object!");

            Destroy(this);
        }

        // Used to aim the bullet
        Transform turret = transform.Find("Turret");
        // From which position should the bullet be fired
        Transform muzzle = turret?.Find("Muzzle");

        // Fail if parts of the tank isn't found
        if (muzzle == null || turret == null)
        {
            Debug.LogError("Couldn't find muzzle or turret!");

            Destroy(this);
        }

        // Cache to lessen garbage
        var wfs = new WaitForSeconds(bulletCoolDownTime);

        // Shoot forever
        while (true)
        {
            // Start angle for the first bullet
            float shotAngle =
                (-bulletSpread / 2f) +
                ((bulletSpread / 2f) / bullets);

            // Fire volley of bullets
            for (int i = 0; i < bullets; i++)
            {
                // Add a new bullet
                // TODO: Don't instantiate all the time
                GameObject go = Instantiate(
                    bullet,
                    muzzle.position,
                    transform.rotation);

                // Bullets shouldn't exist forever
                Destroy(go, bulletLifeTime);

                // Fire in this direction
                Vector2 shotDir =
                    Quaternion.Euler(0, 0, shotAngle) * turret.up;

                go.GetComponent<Rigidbody2D>().velocity =
                    bulletSpeed * shotDir;

                // Calculate angle of next bullet
                shotAngle += bulletSpread / bullets;
            }

            // Wait for next volley of bullets
            yield return wfs;
        }
    }
}
