using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DebugStats : MonoBehaviour
{
    /// <summary>
    /// Debug/stat info
    /// </summary>
    static public int spawned = 0;

    /// <summary>
    /// Show the info in this text object
    /// </summary>
    TMP_Text stats;

    void Start()
    {
        stats = GameObject.Find("Stats").GetComponent<TMP_Text>();

        if (stats is null)
        {
            Debug.LogError("Couldn't find stats");

            Destroy(this);
        }
    }

    private void Update()
    {
        stats.text = $"{spawned}";
    }
}
