using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected AIPath pathfinding;
    [SerializeField] protected float movementSpeed;

    [SerializeField] protected float health;

    [SerializeField] protected float damage;
    [SerializeField] protected float attackCooldown;
    protected float timerAttack = 0f;
    [SerializeField] protected float attackDistance;

    protected PlayerController player;

    public new Renderer[] renderer;

    public enum EnemyState {
        GoingToPlayer,
        PreparingAttack,
        Dead
    }
    [SerializeField] protected EnemyState actualState;

    public static Action<Enemy> EnemyDead;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>();
        pathfinding.maxSpeed = movementSpeed;
        renderer = gameObject.GetComponentsInChildren<Renderer>();
    }

    void OnDestroy() {
        EnemyDead?.Invoke(this);
    }

    protected virtual void Update()
    {
        if (actualState == EnemyState.Dead)
            return;

        pathfinding.destination = player.transform.position;
        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance) {
            pathfinding.canMove = false;
            actualState = EnemyState.PreparingAttack;
        }
        else {
            actualState = EnemyState.GoingToPlayer;
            pathfinding.canMove = true;
            timerAttack = 0f;
        }

        if(actualState == EnemyState.PreparingAttack) {
            timerAttack += Time.deltaTime;
            if(timerAttack >= attackCooldown) {
                timerAttack = 0f;
                AttackPlayer();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (actualState == EnemyState.Dead)
            return;

        health -= damage;
        if (health <= 0f)
        {
            pathfinding.canMove = false;
            actualState = EnemyState.Dead;
            foreach (Renderer rend in renderer)
            {
                rend.material.color = Color.blue;
            }
            Destroy(this.gameObject, 2f);
        }
    }

    public virtual void Hit(float value) {
        if (actualState == EnemyState.Dead)
            return;

        health -= value;
        if (health <= 0f) {
            pathfinding.canMove = false;
            actualState = EnemyState.Dead;
            foreach(Renderer rend in renderer)
            {
                rend.material.color = Color.blue;
            }
            Destroy(this.gameObject, 2f);
        }
    }

    protected virtual void AttackPlayer() {
        player.Hit(damage);
    }


}
