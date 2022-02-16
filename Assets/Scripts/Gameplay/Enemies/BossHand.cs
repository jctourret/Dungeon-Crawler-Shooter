using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    float damage = 0;

    public void SetDamage(float value) {
        damage = value;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerController pc = other.GetComponent<PlayerController>();
            pc.Hit(damage);
        }    
    }
}
