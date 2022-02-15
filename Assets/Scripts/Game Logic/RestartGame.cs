using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public string startButton = "Fire1";

    void Update()
    {
        if (Input.GetButtonDown(startButton))
        {
            if (HandleTank.newHighScore)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
