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
    /// Debug/stat info
    /// </summary>
    TMP_Text stats;

    private void Update()
    {
        stats.text = $"{spawned}";
    }

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("Stats").GetComponent<TMP_Text>();
    }
}
