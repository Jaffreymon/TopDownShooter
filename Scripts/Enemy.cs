using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    [SerializeField]
    private float expOnDeath;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float dealtDamage;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    public override void Die()
    {
        player.AddExperience(expOnDeath);
        base.Die();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            player.takeDamage(dealtDamage);
        }
    }
}
