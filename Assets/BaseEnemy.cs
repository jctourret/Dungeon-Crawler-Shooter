using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    public int health = 255;
    new MeshRenderer renderer;
    Color currentColor;
    private void Start()
    {

        renderer = gameObject.GetComponent<MeshRenderer>();
        currentColor = Color.red;
        renderer.material.color = currentColor;
    }
    public void TakeDamage(int damage)
    {
        health = health - damage;
        currentColor = new Color(health,0,255-health);
    }
}
