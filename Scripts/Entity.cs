using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    [SerializeField]
    protected float health;

    public virtual void takeDamage(float dmg)
    {
        setHealth(health - dmg);

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

    protected void setHealth(float _health)
    {
        health = _health;
    }

    public virtual void addHealth(float _heal)
    {
        setHealth(health + _heal);
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
