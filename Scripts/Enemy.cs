using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    // Experience rewarded to player
    [SerializeField]
    private float expOnDeath;
    // Damage to player
    [SerializeField]
    private float dealtDamage;
    //Range to detect player
    [SerializeField]
    private float attackRange;
    //Attack frequency
    [SerializeField]
    private float attackCooldown;
    // Attack time tracker
    private float nextAttackTime;
    //Speed of attack
    [SerializeField]
    private float attackSpeed;

    private Player player;
    private GameManager GM;
    private AIPath aiPath;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        aiPath = GetComponent<AIPath>();
    }

    /** Damage player if in collision radius
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
    */

    private void Update()
    {
        if (Time.time > nextAttackTime)
        {
            float sqrDistToPlayer = (player.transform.position - transform.position).sqrMagnitude;
            if (sqrDistToPlayer < Mathf.Pow(attackRange, 2))
            {
                nextAttackTime = Time.time + attackCooldown;
                //StartCoroutine(Attack());
            }
        }
    }


    IEnumerator Attack() {
        aiPath.canSearch = false;

        Vector3 origPosition = transform.position;
        Vector3 playerPosition = player.transform.position;

        float percent = 0f;

        while (percent <= 1) { 
            percent += Time.deltaTime * attackSpeed;

            // Curved lunged toward player's position
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            // Damage is done at peak of curve
            if ( interpolation >= 0.99) { player.takeDamage(dealtDamage); }

            transform.position = Vector3.Lerp(origPosition, playerPosition, interpolation);

            yield return null;
        }

        aiPath.canSearch = true;
    }

    public override void Die()
    {
        player.AddExperience(expOnDeath);
        GM.destroyEnemy();
        base.Die();
    }
}
