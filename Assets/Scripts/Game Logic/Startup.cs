using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Startup : MonoBehaviour
{
    public TMP_Text localHighScore;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Music"));

        ShowHighScore();
    }

    private void ShowHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);
        string highScoreName = PlayerPrefs.GetString("HighScoreName", "Uknown");

        localHighScore.text = $"Local Hiro: {highScoreName} {highScore}";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(1);
        }

        if(Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetFloat("HighScore", 0);
            PlayerPrefs.SetString("HighScoreName", "Uknown");
            PlayerPrefs.Save();

            ShowHighScore();
        }
    }
}
