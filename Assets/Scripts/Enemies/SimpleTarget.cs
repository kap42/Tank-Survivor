using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTarget : MonoBehaviour
{
    public string targetName = "Tank";
    public string triggerAnimName = "Flash";

    public float moveSpeed = 2f;

    public float minRandTime = .5f;
    public float maxRandTime = 4f;

    public bool flip = false;
    public float angleAdjust = -90;

    public float maxTurn = 0.05f;

    float randTime = 0;

    Rigidbody2D rb;

    Animator anim;

    Transform target;

    SpriteRenderer sr;

    Vector3 vAngle = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        target = GameObject.Find(targetName)?.transform;
    }

    private void Update()
    {
        Vector2 dir = Vector2.zero;

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
                    Quaternion.Euler(0, 0, newAngle), maxTurn)
                    * Vector2.up;

                vAngle.z = newAngle + Random.Range(-10, 10) - angleAdjust;

                transform.eulerAngles = vAngle;
            }
        }

        rb.velocity = dir.normalized * moveSpeed;

        float angle = Vector2.SignedAngle(Vector2.up, dir);

        vAngle.z = angle + angleAdjust;

        transform.eulerAngles = vAngle;

        if (flip)
        {
            sr.flipY = angle > 20 && angle < 270;
        }
    }
}
