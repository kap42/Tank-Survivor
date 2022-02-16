using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnClusterTimed : MonoBehaviour
{

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
    /// How large should the cluster be?
    /// </summary>
    public float clusterRadius = 4f;

    /// <summary>
    /// The minimum number of objects in the cluster
    /// </summary>
    public int clusterMinSize = 5;
    
    /// <summary>
    /// The maximum number of objects in the cluster
    /// </summary>
    public int clusterMaxSize = 5;

    /// <summary>
    /// Cache the camera
    /// </summary>
    public Transform mainCam = null;
    
    IEnumerator Start()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main.transform;
        }

        // Only use one WFS
        var wfs = new WaitForSeconds(spawnDelay);

        // Forever
        while (true)
        {
            var clusterCenter =
                (Vector2)mainCam.position +
                Random.insideUnitCircle.normalized * radius;

            int clusterSize = Random.Range(clusterMinSize, clusterMaxSize);

            for (int i = 0; i < clusterSize; i++)
            {
                SpawnTimed.spawned++;

                Instantiate(
                    spawnee,
                    clusterCenter +
                    Random.insideUnitCircle.normalized * clusterRadius,
                    spawnee.transform.rotation);
            }

            yield return wfs;
        }
    }
}
