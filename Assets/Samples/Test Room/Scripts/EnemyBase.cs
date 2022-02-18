using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField]
    float health = 100;

    public void TakeDamage(float damage)
    {
        health = health - damage;
        if(health <0)
        {
            Destroy(gameObject);
        }
    }
}
