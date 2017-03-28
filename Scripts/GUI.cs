using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour {

    [SerializeField]
    private  Transform expBarFill;
    [SerializeField]
    private Text levelText;

    public void SetPlayerExperience(float percentToLevel, int playerLevel)
    {
        levelText.text = "level: " + playerLevel;
        expBarFill.localScale = new Vector3(1f, percentToLevel,1f);
    }
}
