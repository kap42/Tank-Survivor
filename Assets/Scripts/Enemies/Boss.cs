using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    /// <summary>
    /// Things to be spawned by the boss
    /// </summary>
    public GameObject spawnee;

    /// <summary>
    /// Delay between spawns
    /// </summary>
    public float spawnDelay = 1;

    /// <summary>
    /// Movement speed of the boss
    /// </summary>
    public float moveSpeed = .5f;

    /// <summary>
    /// Cache the camera
    /// </summary>
    public Transform mainCam;

    /// <summary>
    /// Cache the Rigidbody2D
    /// </summary>
    Rigidbody2D rb;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Cache it
        rb = GetComponent<Rigidbody2D>();

        if (mainCam == null)
        {
            mainCam = Camera.main.transform;
        }

        // Move towards the center of the screen
        var dir = mainCam.position - transform.position;

        rb.velocity = dir.normalized * moveSpeed;

        // Turn the boss in the right direction
        float angle = Vector2.SignedAngle(Vector2.down, rb.velocity);

        transform.eulerAngles = new Vector3(0, 0, angle);

        // Only use one WFS
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
}
