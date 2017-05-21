using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    [SerializeField]
    private float expOnDeath;
    [SerializeField]
    private float dealtDamage;
    [SerializeField]
    private float attackRange;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        // Enemy always looking toward player
        transform.LookAt(player.transform);

        RaycastHit hit;
        Ray enemyKillBox = new Ray(transform.position, transform.forward);

        // Detects if enemy is on "contact" with player
        if (Physics.Raycast(enemyKillBox, out hit, attackRange))
        {
            if (hit.collider.tag == "Player")
            {
                // Damages player on contact
                player.takeDamage(dealtDamage);
            }
        }
    }

    public override void Die()
    {
        player.AddExperience(expOnDeath);
        base.Die();
    }
}
