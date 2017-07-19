using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    private int level;
    private float currExperience;
    private float nextExperienceLvl;

    [SerializeField]
    private GUI_HUD gui;
    [SerializeField]
    private PlayerController playerController;

    void OnEnable()
    {
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI_HUD>();
        playerController = GetComponent<PlayerController>();
        LevelUp();
    }

    void Update()
    {
        gui.SetHealth(this.getHealth());

        if(Input.GetKeyDown(KeyCode.K))
        {
            addHealth(25);
        }
    }

    public void AddExperience(float exp)
    {
        currExperience += exp;

        if(currExperience >= nextExperienceLvl)
        {
            currExperience -= nextExperienceLvl;
            LevelUp();
        }
        gui.SetPlayerExperience(currExperience / nextExperienceLvl, level);
    }

    private void LevelUp()
    {
        level++;
        nextExperienceLvl = level * 50 + Mathf.Pow(level * 2, 2);

        AddExperience(0);
    }

    public int getPlayerLevel()
    {
        return level;
    }

    public override void Die()
    {
        //TODO
        /**
         * Stop player controls
         * Create game over screen
         * Add force to knock player object away on death
         */
        StartCoroutine(playerController.playerKilled());
    }
}
