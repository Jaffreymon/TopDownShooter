using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheatAbility : CharAbility
{
    private Minigun minigun;
    private float lifeTime = 10f;

    private void Start()
    {
        minigun = GetComponentInChildren<Minigun>();
    }

    public override void activate()
    {
        if (isCooldownOver())
        {
            StartCoroutine(activateSkill());
        }
    }

    IEnumerator activateSkill()
    {
        assignCoolDown();
        // Activate skill
        minigun.toggleSkill();
        //Debug.Log("Start time: " + Time.time);

        // Skill is up for some time
        yield return new WaitForSeconds(lifeTime);

        //Debug.Log("End time: " + Time.time);
        // Deactivate skill
        minigun.toggleSkill();
    }
}
