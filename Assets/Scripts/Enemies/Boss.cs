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

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Cache stuff
        mainCam ??= Camera.main.transform;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Move towards the center of the screen
        var dir = mainCam.position - transform.position;

        rb.velocity = dir.normalized * moveSpeed;

        // Turn the boss in the proper direction
        float angle = Vector2.SignedAngle(Vector2.down, rb.velocity);

        transform.eulerAngles = new Vector3(0, 0, angle);

        // Only use one WFS
        var wfs = new WaitForSeconds(spawnDelay);

        while(true)
        {
            yield return wfs;

            // Spawn stuff every now and then
            Instantiate(
                spawnee,
                transform.position,
                transform.rotation);
        }
    }
}
