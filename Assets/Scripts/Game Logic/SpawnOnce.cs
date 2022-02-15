using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnce : MonoBehaviour
{
    public GameObject spawnee;

    public int numToSpawn = 1;

    public float minRadius = 2f;
    public float maxRadius = 200f;

    void Start()
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            Instantiate(
                spawnee,
                Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius),
                spawnee.transform.rotation);
        }
    }
}
