using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimed : MonoBehaviour
{
    /// <summary>
    /// Debug/stat info
    /// </summary>
    static public int spawned = 0;

    /// <summary>
    /// What to spawn
    /// </summary>
    public GameObject spawnee;

    /// <summary>
    /// Spawn within this radius
    /// </summary>
    public float radius = 20f;

    /// <summary>
    /// How long to wait between spawns
    /// </summary>
    public float spawnDelay = 1;

    /// <summary>
    /// Cache the camera
    /// </summary>
    public Transform mainCam;

    /// <summary>
    /// Debug/stat info
    /// </summary>
    private void Update()
    {
        Debug.Log($"{spawned}");
    }

    IEnumerator Start()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main.transform;
        }

        // Only make one copy of the WFS
        var wfs = new WaitForSeconds(spawnDelay);

        // Forever
        while (true)
        {
            // Debug/stat info
            spawned++;

            // Where to spawn
            var pos = Random.insideUnitCircle.normalized * radius;

            // Scale to keep the Y more or less within
            // the same range as X (from the screen center)
            pos.y /= 16f / 9f;

            Instantiate(
                spawnee,
                (Vector2)mainCam.position + pos,
                spawnee.transform.rotation);

            yield return wfs;
        }
    }
}
