using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    float range = 100;
    public Transform muzzle;
    float currentAmmo;
    float maxAmmo;

    int damage = 10;

    public void Fire()
    {
        RaycastHit hit;
        Debug.Log(gameObject.name +" has been fired");
        if(Physics.Raycast(muzzle.position,muzzle.forward,out hit, range))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(damage);
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
