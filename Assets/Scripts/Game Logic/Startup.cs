using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class Startup : MonoBehaviour
{
    public TMP_Text localHighScore;

    void Start()
    {
        // Play music throughout the game
        DontDestroyOnLoad(GameObject.Find("Music"));

        ShowHighScore();
    }

    void Update()
    {
        // Start game
        if(Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(1);
        }

        // Reset high score
        if(Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetFloat("HighScore", 0);
            PlayerPrefs.SetString("HighScoreName", "Unknown");
            PlayerPrefs.Save();

            ShowHighScore();
        }
    }
    private void ShowHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        string highScoreName = PlayerPrefs.GetString("HighScoreName", "Unknown");

        localHighScore.text = $"Local Hiro: {highScoreName} {highScore}";
    }
}
