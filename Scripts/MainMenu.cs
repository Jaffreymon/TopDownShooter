using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartLevel(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
