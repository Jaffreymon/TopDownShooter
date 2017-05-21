using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_HUD : MonoBehaviour {

    [SerializeField]
    private Transform expBarFill;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Text gunNameText;
    [SerializeField]
    private Text healthText;

    public void SetPlayerExperience(float percentToLevel, int playerLevel)
    {
        levelText.text = "Level: " + playerLevel;
        expBarFill.localScale = new Vector3(1f, percentToLevel,1f);
    }

    public void SetAmmoCount(int ammoInMag, int maxMagAmmo)
    {
        ammoText.text = ammoInMag + "/" + maxMagAmmo;
    }

    public void SetCurrGunName(string gunName)
    {
        gunNameText.text = gunName;
    }

    public void SetHealth(float health)
    {
        healthText.text = "Health: " + health;
    }
}
