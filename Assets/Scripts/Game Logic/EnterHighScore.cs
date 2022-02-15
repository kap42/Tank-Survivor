using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterHighScore : MonoBehaviour
{
    public TMP_InputField hsname;

    // Start is called before the first frame update
    void Start()
    {
        hsname.Select();
    }

    public void GetName()
    {
        PlayerPrefs.SetString("HighScoreName", hsname.text);
        PlayerPrefs.Save();

        SceneManager.LoadScene(0);
    }
}
