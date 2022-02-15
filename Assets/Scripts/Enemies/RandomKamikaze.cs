using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RandomKamikaze : MonoBehaviour
{
    /// <summary>
    /// How many objects have been spawned in the scene
    /// </summary>
    public static int spawned = 0;

    /// <summary>
    /// Which prefab should be used for an end explosion?
    /// </summary>
    public GameObject explosion;

    /// <summary>
    /// What should we target?
    /// </summary>
    public string targetName = "Tank";

    /// <summary>
    /// How fast should the enemy move?
    /// </summary>
    public float moveSpeed = 2f;

    /// <summary>
    /// How often should the direction be updated?
    /// </summary>
    public float minRandTime = .1f;
    public float maxRandTime = 1f;

    float randTime = 0;
    float holdTime = 0;

    /// <summary>
    /// Cache the Rigidbody2D
    /// </summary>
    Rigidbody2D rb;

    /// <summary>
    /// Cache the target
    /// </summary>
    Transform target;

    /// <summary>
    /// Cache the SpriteRenderer
    /// </summary>
    SpriteRenderer sr;

    Vector3 vAngle = new Vector3(0, 0, 0);

    Vector2 dir = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        target = GameObject.Find(targetName)?.transform;

        if (target != null)
        {
            dir = target.position - transform.position;
        }
        else
        {
            dir = Random.insideUnitCircle;
        }

        spawned++;

        //        Debug.Log($"Spawed{spawned}");
    }

    void Update()
    {
        // Is it time to change direction?
        if (holdTime < Time.time)
        {
            // Wait for this long until setting a new direction
            holdTime = Time.time + Random.Range(minRandTime, maxRandTime);

            // Have we acquired a target?
            if (target != null)
            {
                dir = target.position - transform.position;
            }
            else
            {
                if (randTime < Time.time)
                {
                    randTime = Time.time + Random.Range(minRandTime, maxRandTime);

                    float newAngle = Vector2.SignedAngle(Vector2.up, dir);

                    newAngle = transform.rotation.eulerAngles.z;

                    dir =
                        Quaternion.Slerp(
                            transform.rotation,
                        Quaternion.Euler(0, 0, newAngle), .05f)
                        * Vector2.up;

                    vAngle.z = newAngle + Random.Range(-10, 10) - 90;

                    transform.eulerAngles = vAngle;
                }
            }
        }

        dir.Normalize();

        float angle = Vector2.SignedAngle(Vector2.up, dir);

        float angle2 = transform.eulerAngles.z - 90;

        Vector2 dir2 = Quaternion.Slerp(
            Quaternion.Euler(0, 0, angle2),
            Quaternion.Euler(0, 0, angle), .01f) * Vector2.up;

        rb.velocity = dir2.normalized * moveSpeed;

        angle = Vector2.SignedAngle(Vector2.up, dir2);

        vAngle.z = angle + 90;

        transform.eulerAngles = vAngle;

        sr.flipY = angle > 20 && angle < 270;
    }
}
