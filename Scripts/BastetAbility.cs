﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastetAbility : CharAbility {
    private Shotgun shotgun;
    private float lifeTime = 10f;

    protected override void Start()
    {
        base.Start();
        shotgun = GetComponentInChildren<Shotgun>();
    }

    public override void activate()
    {
        if (isSkillActive()) {
            StartCoroutine(activateSkill());
        }
    }

    IEnumerator activateSkill()
    {
        // Activate skill
        assignCoolDown();
        shotgun.toggleSkill();

        //TODO activate skill music

        // Damage increase and ammo reset
        float tmpDamage = shotgun.getDamage();
        shotgun.setGunDamage(tmpDamage * 1.5f);
        shotgun.setGunAmmo(shotgun.getAmmoCount());

        // Skill is up for some time
        yield return new WaitForSeconds(lifeTime);

        // Deactivate skill
        //TODO deactivate skill music
        shotgun.toggleSkill();
        shotgun.setGunDamage(tmpDamage);
        getSkillUI().toggleUI(false);
    }
}
