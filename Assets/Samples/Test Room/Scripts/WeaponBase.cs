using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField]
    float range = 10;
    public Transform muzzle;
    float currentAmmo;
    float maxAmmo;

    int damage = 10;
    int headshotDamage = 20;

    public void Fire()
    {
        RaycastHit hit;
        Debug.Log(gameObject.name +" has been fired");
        if(Physics.Raycast(muzzle.position,muzzle.forward,out hit, range))
        {
            IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
            if(damageable != null)
            {
                if (hit.collider.tag == "EnemyBody")
                {
                    damageable.TakeDamage(damage);
                }
                else
                {
                    damageable.TakeDamage(headshotDamage);
                }
            }
            Debug.Log(hit.collider.name + " has been shot");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(muzzle.position,muzzle.forward*range);
    }
}
