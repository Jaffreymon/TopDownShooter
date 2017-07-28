using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

    // System Setting booleans
    [SerializeField]
    public static bool inOptions = false;

    [SerializeField]
    private GameObject gameGUI;
    [SerializeField]
    private GameObject gameMenu;

    private void OnEnable()
    {
        inOptions = false;
        Time.timeScale = 1;
    }

    public void pause()
    {
        gameGUI.SetActive(inOptions);
        Time.timeScale = ( inOptions = !inOptions) ? 0 : 1;
        Cursor.visible = inOptions;
        gameMenu.SetActive(inOptions);
        Debug.Log(inOptions);
    }

    public bool isPaused()
    {
        return inOptions;
    }

    public void loadLevel(string _levelName)
    {
        if (_levelName == "Restart")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(_levelName);
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
