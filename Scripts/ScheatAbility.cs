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
    }

    public override void activate()
    {
        if (isSkillActive())
        {
            StartCoroutine(activateSkill());
        }
    }

    IEnumerator activateSkill()
    {
        assignCoolDown();
        // Activate skill
        //TODO activate skill music
        minigun.toggleSkill();

        // Skill is up for some time
        yield return new WaitForSeconds(lifeTime);

        // Deactivate skill
        //TODO deactivate skill music
        minigun.toggleSkill();
        getSkillUI().toggleUI(false);
    }
}
