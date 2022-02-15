using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimed : MonoBehaviour
{
    static public int spawned = 0;

    public GameObject spawnee;

    public float radius = 20f;

    public float spawnDelay = 1;

    private void Update()
    {
        Debug.Log($"{spawned}");
    }

    IEnumerator Start()
    {
        var wfs = new WaitForSeconds(spawnDelay);

        while(true)
        {
            spawned++;

            Instantiate(
                spawnee,
                (Vector2)Camera.main.transform.position +
                Random.insideUnitCircle.normalized * radius,
                spawnee.transform.rotation);

            yield return wfs;
        }
    }
}
