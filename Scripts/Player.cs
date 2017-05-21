using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    private int level;
    private float currExperience;
    private float nextExperienceLvl;

    [SerializeField]
    private GUI_HUD gui;

    void Start()
    {
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI_HUD>();
        LevelUp();
    }

    void Update()
    {
        gui.SetHealth(this.getHealth());
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

    public override void Die()
    {
        Debug.Log("Here");
    }
}
