using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Kamikaze : MonoBehaviour
{
    /// <summary>
    /// What should be spawned after trigger time
    /// </summary>
    public GameObject explosion;

    public float moveSpeed = 2f;

    public float squaredTriggerDistance = 25f;
    public float detonateTime = 2f;

    public float minRandTime = .5f;
    public float maxRandTime = 4f;

    public string targetName = "Tank";
    public string triggerAnimName = "Flash";

    float randTime = 0;

    Rigidbody2D rb;

    Animator anim;

    Transform target;

    SpriteRenderer sr;

    Vector3 vAngle = new Vector3(0, 0, 0);

    Vector2 dir = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        target = GameObject.Find(targetName)?.transform;
    }

    void Update()
    {
        if (target != null)
        {
            dir = target.position - transform.position;

            if (dir.sqrMagnitude <= squaredTriggerDistance)
            {
                anim.SetTrigger(triggerAnimName);

                Invoke("Explode", detonateTime);
            }
        }
        else
        {
            if (randTime < Time.time)
            {
                randTime = Time.time + Random.Range(minRandTime, maxRandTime);

                float newAngle = Vector2.SignedAngle(Vector2.up, dir);

                dir =
                    Quaternion.Slerp(
                        transform.rotation,
                        Quaternion.Euler(0, 0, newAngle), .05f)
                    * Vector2.up;

                newAngle = transform.rotation.eulerAngles.z;

                vAngle.z = newAngle + Random.Range(-10, 10) - 90;

                transform.eulerAngles = vAngle;

                dir = transform.up;
            }
        }

        rb.velocity = dir.normalized * moveSpeed;

        float angle = Vector2.SignedAngle(Vector2.up, dir);

        vAngle.z = angle + 90;

        transform.eulerAngles = vAngle;

        sr.flipY = angle > 20 && angle < 270;
    }

    /// <summary>
    /// Kills the object and start an explosion
    /// </summary>
    void Explode()
    {
        Instantiate(
            explosion,
            transform.position,
            Quaternion.identity);

        Destroy(gameObject);
    }
}
