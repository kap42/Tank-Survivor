using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject spawnee;

    public float spawnDelay = 1;

    public float moveSpeed = .5f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 dir = Camera.main.transform.position - transform.position;

        rb.velocity = dir.normalized * moveSpeed;

        float angle = Vector2.SignedAngle(Vector2.down, rb.velocity);

        transform.eulerAngles = new Vector3(0, 0, angle);

        var wfs = new WaitForSeconds(spawnDelay);

        while(true)
        {
            yield return wfs;

            Instantiate(
                spawnee,
                transform.position,
                transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
