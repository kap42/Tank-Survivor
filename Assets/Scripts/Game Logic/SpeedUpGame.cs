using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpGame : MonoBehaviour
{
    /// <summary>
    /// Factor to speed up game with each frame
    /// </summary>
    public float speedUp = 0.0001f;

    void Update()
    {
        // Speed up game
        Time.timeScale += speedUp;    
    }
}
