using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
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
    /// Movement speed of the bullets
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

    /// <summary>
    /// If true the turrent will aim at the closest enemy
    /// </summary>
    public bool aim = true;

    /// <summary>
    /// Which objects should be targeted?
    /// </summary>
    public LayerMask includeMask;

    /// <summary>
    /// How often should the turret reaim
    /// </summary>
    public float aimDelay = 1;

    /// <summary>
    /// How far away can an enemy be
    /// </summary>
    public float aimDistance = 20f;

    private float nextAim = 0;
    private Vector3 vAngle = Vector3.zero;

    private Transform turret;
    private float targetAngle = 0;

    void Update()
    {
        Vector2 dir = Quaternion.Slerp(
            turret.rotation,
            Quaternion.Euler(0, 0, targetAngle), .025f) * Vector2.up;

        float angle = Vector2.SignedAngle(Vector2.up, dir);

        vAngle.z = angle;

        turret.eulerAngles = vAngle;
    }

    IEnumerator Start()
    {
        // Fail if the bullet isn't set
        // Used to aim the bullet
        if (bullet == null)
        {
            Debug.LogError("Bullet isn't set to an object!");

            Destroy(this);
        }

        // Find the angle for the bullet
        turret = transform.Find("Turret");
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

        var possibleTargets = new List<Collider2D>();

        var cf = new ContactFilter2D();

        cf.SetLayerMask(includeMask);

        GameObject currentTarget = null;

        // Shoot forever
        while (true)
        {
            if (aim)
            {
                if (nextAim < Time.time)
                {

                    nextAim = Time.time + aimDelay;

                    int targetsFound = Physics2D.OverlapCircle(
                        transform.position, aimDistance,
                        cf,
                        possibleTargets);

                    if (targetsFound > 0)
                    {
                        currentTarget = possibleTargets[0].gameObject;
                        float shortestDistance = (currentTarget.transform.position - transform.position).sqrMagnitude;

                        for (int i = 1; i < targetsFound; i++)
                        {
                            float currDistance = (possibleTargets[i].transform.position - transform.position)
                                .sqrMagnitude;

                            if (currDistance < shortestDistance)
                            {
                                shortestDistance = currDistance;
                                currentTarget = possibleTargets[i].gameObject;
                            }
                        }

                        var dir = currentTarget.transform.position - turret.position;

                        targetAngle = Vector2.SignedAngle(Vector2.up, dir.normalized);
                    }
                    else
                    {
                        targetAngle = 0;
                    }
                }
            }

            // Start angle for the first bullet
            float shotAngle =
            (-bulletSpread / 2f) +
            ((bulletSpread / 2f) / bullets);

            // Fire volley of bullets
            for (int i = 0; i < bullets; i++)
            {
                // Add a new bullet
                // TODO: Use object pooling
                var go = Instantiate(
                    bullet,
                    muzzle.position,
                    transform.rotation);

                // Bullets shouldn't exist forever
                Destroy(go, bulletLifeTime);

                // Fire in this direction
                var shotDir =
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
