using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected AIPath pathfinding;
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackCooldown;
    protected PlayerController player;
    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>();
        pathfinding.destination = player.transform.position;
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
