using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnClusterTimed : MonoBehaviour
{
    public GameObject spawnee;

    public float radius = 20f;

    public float clusterRadius = 4f;

    public int clusterSize = 5;

    public float spawnDelay = 1;

    IEnumerator Start()
    {
        var wfs = new WaitForSeconds(spawnDelay);

        while (true)
        {
            Vector2 clusterCenter =
                (Vector2)Camera.main.transform.position +
                Random.insideUnitCircle.normalized * radius;

            for (int i = 0; i < clusterSize; i++)
            {
                Instantiate(
                    spawnee,
                    clusterCenter + Random.insideUnitCircle.normalized*clusterRadius,
                    spawnee.transform.rotation);
            }

            yield return wfs;
        }
    }
}
