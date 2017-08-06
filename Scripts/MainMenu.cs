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
    private Text resolutionLabel;

    [SerializeField]
    private Toggle fullscreenToggle;
    [SerializeField]
    private Image checkmark;

    [SerializeField]
    private Dropdown textureQualityOption;
    [SerializeField]
    private Text textureLabel;

    [SerializeField]
    private Slider sensitivityOption;
    [SerializeField]
    private Slider volumeOption;

    private Resolution[] userResolutions;
    private MusicManager musicManager;
    private GameObject gameHUD;

    private void OnEnable()
    {
        inOptions = false;
        resolutionOption.ClearOptions();

        // Fetches game's music manager
        musicManager = GameObject.FindGameObjectWithTag("musicManager").GetComponent<MusicManager>();
        // Fetches all supported screen resolutions
        userResolutions = Screen.resolutions;

        foreach (Resolution resolution in userResolutions)
        {
         resolutionOption.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        // Gets saved sensitivity option and applies
        sensitivityOption.value = PlayerPrefs.GetFloat("RotationSensitivity", 4000f);

        // Gets saved music volume option and applies
        volumeOption.value = PlayerPrefs.GetFloat("volumeChange", 0.1f);
        musicManager.setVolume(volumeOption.value);

        // Gets saved resolution option and applies; Defaults to second highest resolution option
        resolutionOption.value = PlayerPrefs.GetInt("userResolution", userResolutions.Length - 2);
        resolutionChange();

        // Gets saved texture option and applies
        textureQualityOption.value = PlayerPrefs.GetInt("userTexture", 0);
        textureQualityChange();

        // Gets saved fullscreen option and applies
        if (PlayerPrefs.GetInt("fullscreenOption", 0) == 0)
        {
            fullscreenToggle.isOn = false;
        }
    }

    public void loadLevel(string _levelName)
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
        Screen.fullScreen = fullscreenToggle.isOn;
        PlayerPrefs.SetInt("fullscreenOption", (fullscreenToggle.isOn) ? 1 : 0);
    }

    public void resolutionChange()
    {
        Screen.SetResolution(userResolutions[resolutionOption.value].width, userResolutions[resolutionOption.value].height, Screen.fullScreen);
        resolutionLabel.text = userResolutions[resolutionOption.value].ToString();
        PlayerPrefs.SetInt("userResolution", resolutionOption.value);
    }

    public void textureQualityChange()
    {
        QualitySettings.masterTextureLimit = textureQualityOption.value;
        textureLabel.text = getTextureName(QualitySettings.masterTextureLimit);
        PlayerPrefs.SetInt("userTexture", textureQualityOption.value);
    }

    public void sensitivityChange()
    {
        PlayerPrefs.SetFloat("RotationSensitivity",sensitivityOption.value);
    }

    public void musicVolumeChange()
    {
        PlayerPrefs.SetFloat("volumeChange", volumeOption.value);
        musicManager.setVolume(volumeOption.value);
    }

    private string getTextureName(int _textureQuality)
    {
        switch(_textureQuality)
        {
            case 0:
                return "High";
            case 1:
                return "Medium";
            default:
                return "Low";
        }
    }
}
