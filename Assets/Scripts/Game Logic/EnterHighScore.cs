using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class EnterHighScore : MonoBehaviour
{
    public TMP_InputField hsname;

    void Start()
    {
        // Select/activate the text field.
        hsname.Select();
    }

    /// <summary>
    /// Runs once the user hits enter
    /// </summary>
    public void GetName()
    {
        PlayerPrefs.SetString("HighScoreName", hsname.text);
        PlayerPrefs.Save();

        SceneManager.LoadScene(0);
    }
}
