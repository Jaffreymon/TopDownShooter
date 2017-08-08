using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheatAbility : CharAbility
{
    private Minigun minigun;
    private float lifeTime = 10f;

    protected override void Start()
    {
        base.Start();
        minigun = GetComponentInChildren<Minigun>();
        skillDmgMultiplier = 1.7f;
    }

    public override void activate()
    {
        if (isSkillActive())
        {
            StartCoroutine(activateSkill());
        }
    }

    public override void modifyDmgMultiplier(int _playerLvl)
    {
        if (_playerLvl < maxLvl)
        {
            skillDmgMultiplier *= Mathf.Exp((_playerLvl - 1) / 2);
        }
    }

    IEnumerator activateSkill()
    {
        // Activate skill
        assignCoolDown();
        minigun.toggleSkill();

        //TODO activate skill music
        float tmpDamage = minigun.getDamage();
        minigun.setGunDamage(tmpDamage * skillDmgMultiplier);
        minigun.setGunAmmo(minigun.getAmmoCount());

        // Skill is up for some time
        yield return new WaitForSeconds(lifeTime);

        // Deactivate skill
        //TODO deactivate skill music
        minigun.toggleSkill();
        minigun.setGunDamage(tmpDamage);
    }
}
