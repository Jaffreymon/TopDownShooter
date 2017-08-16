using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    private int level;
    private float maxHealth = 100f;
    private const float extraHealthOnLvl = 3f;
    private float currExperience;
    private float nextExperienceLvl;

    public string characterName;
    [SerializeField]
    private CharAbility ability;
    [SerializeField]
    private GUI_HUD gui;
    [SerializeField]
    private PlayerController playerController;

    void OnEnable()
    {
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI_HUD>();
        playerController = GetComponent<PlayerController>();
        health = maxHealth;
        LevelUp();
    }

    void Update()
    {
        if (Input.GetButtonDown("Ability"))
        {
            activateAbility();
        }

        gui.SetHealth(getHealth());
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
        maxHealth += extraHealthOnLvl;
        nextExperienceLvl = level * 50 + Mathf.Pow(level * 2, 2);
        ability.modifyDmgMultiplier(level);

        AddExperience(0);
    }

    public int getPlayerLevel()
    {
        return level;
    }

    public override void Die()
    {
        StartCoroutine(playerController.playerKilled());
    }

    public override void addHealth(float _heal)
    { 
        // Heals if player is damaged
        if (getHealth() < maxHealth) {
            // Heals player capping at max health
            base.addHealth( (getHealth() + _heal > maxHealth) ? maxHealth - getHealth(): _heal );
        }
        // Powers up player level if healthy
        else
        {
            LevelUp();
        }
    }

    public void setName(string _name) { characterName = _name; }

    public string getName() { return characterName; }

    public void activateAbility() { ability.activate(); }
}
