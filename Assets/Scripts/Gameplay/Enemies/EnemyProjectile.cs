using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] PlayerController player;
    [SerializeField] Vector3 direction;

    bool hitted = false;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
       Destroy(this.gameObject, 5);
    }

    void Update() {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void SetProjectile(Vector3 dir, float dmg) {
        direction = dir;
        transform.LookAt(transform.position + direction);
        damage = dmg;
    }

    private void OnTriggerEnter(Collider other) {
        if (hitted)
            return;

        if (other.CompareTag("Player")) { 
            player.Hit(damage);
            hitted = true;
            Destroy(this.gameObject);
        }
    }
}
