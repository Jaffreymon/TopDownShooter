using System.Collections;
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
        assignCoolDown();
        // Activate skill
        //TODO activate skill music
        shotgun.toggleSkill();

        // Skill is up for some time
        yield return new WaitForSeconds(lifeTime);

        // Deactivate skill
        //TODO deactivate skill music
        shotgun.toggleSkill();
        getSkillUI().toggleUI(false);
    }
}
