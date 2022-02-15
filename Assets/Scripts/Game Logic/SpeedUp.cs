using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float speedUp = 0.0001f;

    void Update()
    {
        Time.timeScale += speedUp;    
    }
}
