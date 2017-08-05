using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SkillUI : MonoBehaviour {
    [SerializeField]
    private Image skillUIHolder;
    [SerializeField]
    private Text skillUITimer;
    private Color skillReadyColor = Color.white;
    // Translucent gray color to indicate skill on cooldown
    private Color onCooldownColor = new Color(200f, 200f, 200f, 0.5f);

    public void updateTimer(float _time)
    {
        skillUITimer.text = "" + Mathf.Round(_time);
    }

    public void toggleUI(bool _isOnCooldown)
    {
        if(_isOnCooldown)
        {
            skillUIHolder.color = onCooldownColor;
        }
        else
        {
            skillUIHolder.color = skillReadyColor;
            skillUITimer.text = "Ready";
        }
    }
}
