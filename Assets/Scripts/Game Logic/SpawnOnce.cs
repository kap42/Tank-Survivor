using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnce : MonoBehaviour
{
    /// <summary>
    /// What to spawn
    /// </summary>
    public GameObject spawnee;

    /// <summary>
    /// Number of objects to spawn
    /// </summary>
    public int numToSpawn = 1;

    /// <summary>
    /// At least this long from the camera
    /// </summary>
    public float minRadius = 2f;

    /// <summary>
    /// At most this long from the camera
    /// </summary>
    public float maxRadius = 200f;

    /// <summary>
    /// Cache the camera
    /// </summary>
    public Transform mainCam;

    void Start()
    {
        mainCam ??= Camera.main.transform;

        for (int i = 0; i < numToSpawn; i++)
        {
            DebugStats.spawned++;

            Instantiate(
                spawnee,
                (Vector2)mainCam.position +
                Random.insideUnitCircle.normalized *
                Random.Range(minRadius, maxRadius),
                spawnee.transform.rotation);
        }
    }
}
