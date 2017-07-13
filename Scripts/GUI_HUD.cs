﻿using System.Collections;
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

    public void SetCurrGunName(string gunName)
    {
        
    }

    public void SetHealth(float health)
    {
        healthCountText.text = "Health: " + health;
    }
}