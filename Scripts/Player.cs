﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    private int level;
    private float currExperience;
    private float nextExperienceLvl;

    [SerializeField]
    private GUI gui;

    void Start()
    {
        gui = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUI>();
        LevelUp();
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
}
