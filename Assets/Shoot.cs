using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    RaycastHit hit;
    float gunRange = 100f;
    int weaponDamage = 10;
    public void shoot()
    {
        if(Physics.Raycast(transform.position,transform.forward, out hit, gunRange))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(weaponDamage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position,transform.forward*10);
    }
}
