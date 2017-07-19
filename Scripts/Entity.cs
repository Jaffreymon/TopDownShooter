using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    [SerializeField]
    private float health;

    public virtual void takeDamage(float dmg)
    {
        health -= dmg;

        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public float getHealth()
    {
        return health;
    }

    protected void addHealth(float _heal)
    {
        health += _heal;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
