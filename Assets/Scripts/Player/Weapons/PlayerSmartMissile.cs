using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmartMissile : MonoBehaviour
{
    /// <summary>
    /// Only target objects within these layers
    /// </summary>
    public LayerMask includeMask;

    /// <summary>
    /// The missile's speed
    /// </summary>
    public float moveSpeed = 10f;

    /// <summary>
    /// Start in a random direction for this long
    /// </summary>
    public float startUpDelay = 1f;

    /// <summary>
    /// How often should the missile search for a closer target?
    /// </summary>
    public float retargetDelay = 2f;

    /// <summary>
    /// The next time to perform a retarget
    /// </summary>
    float retargetTime;

    IEnumerator Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Start by going in a random direction for a short while
        Vector2 randDir = Random.insideUnitCircle.normalized;

        rb.velocity = randDir * moveSpeed;

        float randAngle = Vector2.SignedAngle(Vector2.up, randDir);

        transform.eulerAngles = new Vector3(0, 0, randAngle);

        // Maintain random direction awhile.
        yield return new WaitForSeconds(startUpDelay);

        var possibleTargets = new List<Collider2D>();

        GameObject currentTarget = null;
        Vector2 dir;

        Vector3 vAngle = new Vector3(0, 0, 0);

        while (true)
        {
            if (currentTarget == null || retargetTime < Time.time)
            {
                retargetTime = Time.time + retargetDelay;

                var cf = new ContactFilter2D();

                cf.SetLayerMask(includeMask);

                int targetsFound = Physics2D.OverlapCircle(
                    transform.position, 20f,
                    cf,
                    possibleTargets);

                if (targetsFound > 0)
                {
                    currentTarget = possibleTargets[0].gameObject;
                    float shortestDistance = (currentTarget.transform.position - transform.position).sqrMagnitude;

                    for (int i = 1; i < targetsFound; i++)
                    {
                        float currDistance = (possibleTargets[i].transform.position - transform.position).sqrMagnitude;

                        if (currDistance < shortestDistance)
                        {
                            shortestDistance = currDistance;
                            currentTarget = possibleTargets[i].gameObject;
                        }
                    }
                }
            }
            else
            {
                dir = currentTarget.transform.position - transform.position;
                dir.Normalize();

                float angle = Vector2.SignedAngle(Vector2.up, dir);

                dir = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.Euler(0, 0, angle), .025f) * Vector2.up;

                rb.velocity = dir * moveSpeed;

                angle = Vector2.SignedAngle(Vector2.up, dir);

                vAngle.z = angle;

                transform.eulerAngles = vAngle;
            }

            yield return null;
        }
    }
}
