using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimed : MonoBehaviour
{
    public GameObject spawnee;

    public float radius = 20f;

    public float spawnDelay = 1;

    IEnumerator Start()
    {
        var wfs = new WaitForSeconds(spawnDelay);

        while(true)
        {
            Instantiate(
                spawnee,
                (Vector2)Camera.main.transform.position +
                Random.insideUnitCircle.normalized * radius,
                spawnee.transform.rotation);

            yield return wfs;
        }
    }
}
