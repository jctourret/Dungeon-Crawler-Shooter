using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField]
    int health = 100;

    public void TakeDamage(int damage)
    {
        health = health - damage;
        if(health <0)
        {
            Destroy(gameObject);
        }
    }
}
