using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected float damage;
    [SerializeField] protected float headshotMultiplier;

    [SerializeField] protected float weaponRange;

    [SerializeField] protected Transform cannonPos;

    [SerializeField] protected float shootCooldown;
    protected float timerShootCooldown = 0;

    [SerializeField] protected float maxHeatBuilup;
    [SerializeField] protected float currentHeat;
    [SerializeField] protected float heatBuildupRate;
    [SerializeField] protected float heatDispersionRate;

    [SerializeField] protected GameObject particlesHitPrefab;
    [SerializeField] protected GameObject bulletHolePrefab;

    [SerializeField] protected float reloadTime;
    protected float timerReloading = 0;

    [SerializeField] protected LayerMask layerEnemy;


    protected enum WeaponState {
        Ready, Charging, Cooling
    }
    [SerializeField] protected WeaponState actualState;

   protected virtual void Update()
   {
        Debug.DrawRay(cannonPos.position, cannonPos.forward * weaponRange, Color.yellow);
        if(currentHeat > 0.0f)
        {
            currentHeat -= Time.deltaTime * heatDispersionRate;
        }
        switch (actualState) {
            case WeaponState.Cooling:
                if(currentHeat <= 0) {
                    actualState = WeaponState.Ready;
                }
                break;
            case WeaponState.Charging:
                timerShootCooldown += Time.deltaTime;
                if(timerShootCooldown >= shootCooldown) {
                    actualState = WeaponState.Ready;
                    timerShootCooldown = 0f;
                }
                break;
        }
   }

    public virtual void Shoot() {
        if (actualState == WeaponState.Charging || actualState == WeaponState.Cooling) 
            return;

        if (actualState == WeaponState.Ready) {
            RaycastHit hit;

            if (Physics.Raycast(cannonPos.position, cannonPos.forward, out hit, weaponRange)) {

                if (layerEnemy == (layerEnemy | (1 << hit.transform.gameObject.layer))) {
                    IDamageable damageable = hit.transform.GetComponentInParent<IDamageable>();

                    if (damageable != null) {
                        if (hit.transform.CompareTag("EnemyHead")) {
                            Debug.Log("PUM HEADSHOT");
                            damageable.TakeDamage(damage * headshotMultiplier);
                        }
                        else
                        {
                            damageable.TakeDamage(damage);
                        }
                    }

                    GameObject ps = Instantiate(particlesHitPrefab, hit.point, particlesHitPrefab.transform.rotation);
                    Destroy(ps, 1);
                }
                else {
                    GameObject hole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                    Destroy(hole, 5f);
                }
            }

            actualState = WeaponState.Charging;

            currentHeat += heatBuildupRate;
            if (currentHeat >= maxHeatBuilup)
            {
                actualState = WeaponState.Cooling;
            }
        }
    }

    public virtual void Reload() {
        if (actualState != WeaponState.Cooling) {
            actualState = WeaponState.Cooling;
            timerReloading = 0f;
        }
    }





}
