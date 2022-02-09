using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackCooldown;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    public virtual void Hit(float value) {
        health -= value;
        if (health <= 0f)
            Destroy(this.gameObject);
    }



}
