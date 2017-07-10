using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

    // System Setting booleans
    private bool inOptions;

    [SerializeField]
    private GameObject gameGUI;
    [SerializeField]
    private GameObject gameMenu;

    // Use this for initialization
    void OnEnable () {
        inOptions = false;
        Time.timeScale = 1;
	}


    public void pause()
    {
        gameGUI.SetActive(inOptions);
        Time.timeScale = ( inOptions = !inOptions) ? 0 : 1;
        Cursor.visible = inOptions;
        gameMenu.SetActive(inOptions);
    }

    public void StartLevel(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
