using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public GameObject mine;

    public float moveSpeed = 5;

    public float minDropTime = .5f;
    public float maxDropTime = 2f;

    IEnumerator Start()
    {
        Vector2 dir;

        GameObject go = GameObject.Find("Tank");

        if (go != null)
        {
            dir = go.transform.position - transform.position;
        }
        else
        {
            dir = Random.insideUnitCircle;
        }

        dir.Normalize();

        GetComponent<Rigidbody2D>().velocity = dir * moveSpeed;

        while (true)
        {
            Instantiate(
                mine,
                transform.position,
                Quaternion.identity
                );

            yield return new WaitForSeconds(Random.Range(minDropTime, maxDropTime));
        }
    }
}
