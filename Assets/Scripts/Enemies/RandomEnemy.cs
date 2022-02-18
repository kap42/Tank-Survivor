using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public float maxMagnitude = 10f;

    public float minDelay = .5f;
    public float maxDelay = 2f;

    public float maxAway = 400;

    public float angleAdd = 90;

    public float flipMin = 20;
    public float flipMax = 270;

    Rigidbody2D rb;

    SpriteRenderer sr;

    public static WaitForSeconds[] wfs = {
        new WaitForSeconds(0.5f),
        new WaitForSeconds(1f),
        new WaitForSeconds(1.5f),
        new WaitForSeconds(2f),
        new WaitForSeconds(2.5f),
        new WaitForSeconds(3f),
        new WaitForSeconds(3.5f),
        new WaitForSeconds(4f),
        new WaitForSeconds(4.5f),
        new WaitForSeconds(5f),
        };

    private Transform mainCam = null;

    IEnumerator Start()
    {
        mainCam ??= Camera.main.transform;

        sr = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();

        Vector2 dir;

        while (true)
        {
            dir = mainCam.position - transform.position;

            if (dir.sqrMagnitude > maxAway)
            {
                rb.velocity = dir.normalized * maxMagnitude;
            }
            else
            {
                rb.velocity = Random.insideUnitCircle * maxMagnitude;
            }

            float angle = Vector2.SignedAngle(Vector2.up, rb.velocity);

            transform.eulerAngles = new Vector3(0, 0, angleAdd + angle);

            sr.flipY = angle > flipMin && angle < flipMax;

            yield return wfs[Random.Range(0, wfs.Length)];
        }
    }
}
