using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float maxMagnitude = 10f;

    public float minDelay = .5f;
    public float maxDelay = 2f;

    public float maxAway = 81;

    public float radius = 2;

    Rigidbody2D rb;

    private Transform dronePlane;

    private Vector3 size = Vector3.one;

    public static WaitForSeconds[] wfs = {
        new WaitForSeconds(0.25f),
        new WaitForSeconds(.5f),
        new WaitForSeconds(.75f),
        new WaitForSeconds(1f),
    };

    private Animator anim;

    private float waiting = 0;

    void Update()
    {
        if (waiting < Time.time)
        {
            waiting = Time.time + 1f;

            anim.speed = Random.Range(.1f, 1.5f);
        }
    }

    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.enabled = false;

        dronePlane = transform.Find("Drone Plane");

        Vector2 dir;

        float startSize = dronePlane.localScale.x;
        for (float f = 0; f < 1.1f; f += .1f)
        {
            dronePlane.localScale = size * Mathf.Lerp(0, startSize, f);
            
            yield return null;
        }

        for (float f = 0; f < 1.1f; f += .1f)
        {
            Vector2 pos = dronePlane.localPosition;

            pos.x = Mathf.Lerp(0, radius, f);

            dronePlane.localPosition = pos;

            yield return null;
        }

        anim.enabled = true;

        anim.speed = Random.Range(.1f, 1.5f);

        while (true)
        {
            dir = Camera.main.transform.position - transform.position;

            if (dir.sqrMagnitude > maxAway)
            {
                rb.velocity = dir.normalized * maxMagnitude;
            }
            else
            {
                rb.velocity = Random.insideUnitCircle * maxMagnitude;
            }

            float angle = Vector2.SignedAngle(Vector2.up, rb.velocity);

            transform.eulerAngles = new Vector3(0, 0, 90 + angle);

            yield return wfs[Random.Range(0, wfs.Length)];
        }
    }
}
