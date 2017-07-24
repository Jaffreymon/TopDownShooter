using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_HUD : MonoBehaviour {

    [SerializeField]
    private Text playerLevelText;
    [SerializeField]
    private GameObject expBarFill;
    [SerializeField]
    private Slider expBar;
    [SerializeField]
    private Text ammoCountText;
    [SerializeField]
    private Text healthCountText;
    [SerializeField]
    private Text dayCountText;
    [SerializeField]
    private Text daySurvivedText;

    [SerializeField]
    private GameObject aliveHUD;
    [SerializeField]
    private GameObject deadScreen;
    private int lastDaySaved;

    public void SetPlayerExperience(float percentToLevel, int playerLevel)
    {
        playerLevelText.text = "Level: " + playerLevel;
        expBar.value = percentToLevel;
        
        if(expBar.value == 0)
        {
            expBarFill.SetActive(false);
        }
        else
        {
            expBarFill.SetActive(true);
        }
    }

    public void SetAmmoCount(int ammoInMag, int maxMagAmmo)
    {
        ammoCountText.text = "Ammo: " + ammoInMag + "/" + maxMagAmmo;
    }

    public void SetHealth(float health)
    {
        healthCountText.text = "Health: " + health;
    }

    public void SetDayCount(int _dayCount)
    {
        lastDaySaved = _dayCount;
        dayCountText.text = "Day: " + _dayCount;
    }

    public void DeathScreen()
    {
        Cursor.visible = true;
        aliveHUD.SetActive(false);
        daySurvivedText.text = "Days Survived: " + lastDaySaved; 
        deadScreen.SetActive(true);
    }
}
