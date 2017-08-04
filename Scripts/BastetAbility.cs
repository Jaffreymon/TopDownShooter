using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastetAbility : CharAbility {
    private Shotgun shotgun;
    private float lifeTime = 10f;

    private void Start()
    {
        shotgun = GetComponentInChildren<Shotgun>();
    }

    public override void activate()
    {
        if (isCooldownOver()) {
            StartCoroutine(activateSkill());
        }
    }

    IEnumerator activateSkill()
    {
        assignCoolDown();
        // Activate skill
        shotgun.toggleSkill();
        //Debug.Log("Start time: " + Time.time);

        // Skill is up for some time
        yield return new WaitForSeconds(lifeTime);
        
        //Debug.Log("End time: " + Time.time);
        // Deactivate skill
        shotgun.toggleSkill();
    }
}
