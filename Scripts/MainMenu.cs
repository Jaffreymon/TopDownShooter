using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // System Setting booleans
    private bool inOptions;

    [SerializeField]
    private GameObject gameMenu;
    [SerializeField]
    private GameObject optionMenu;
    [SerializeField]
    private Dropdown resolutionOption;
    [SerializeField]
    private Toggle fullscreenToggle;
    [SerializeField]
    private Dropdown textureQualityOption;

    private Resolution[] userResolutions;
    private GameObject gameHUD;

    private void OnEnable()
    {
        inOptions = false;
        resolutionOption.ClearOptions();
        fullscreenToggle.isOn = Screen.fullScreen;
        userResolutions = Screen.resolutions;

        foreach (Resolution resolution in userResolutions)
        {
         resolutionOption.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
    }

    public void StartLevel(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void toggleOptionsMenu()
    {
        gameMenu.SetActive(inOptions);
        optionMenu.SetActive(inOptions = !inOptions);
    }

    public void toggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void resolutionChange()
    {
        Screen.SetResolution(userResolutions[resolutionOption.value].width, userResolutions[resolutionOption.value].height, Screen.fullScreen);
    }

    public void textureQualityChange()
    {
        QualitySettings.masterTextureLimit = textureQualityOption.value;
    }
}
