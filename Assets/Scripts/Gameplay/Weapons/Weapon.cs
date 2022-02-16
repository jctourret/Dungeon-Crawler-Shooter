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

    [SerializeField] protected int ammoPerMagazine;
    [SerializeField] protected int actualAmmo;

    [SerializeField] protected GameObject particlesHitPrefab;
    [SerializeField] protected GameObject bulletHolePrefab;

    [SerializeField] protected float reloadTime;
    protected float timerReloading = 0;

    [SerializeField] protected LayerMask layerEnemy;


    protected enum WeaponState {
        Ready, Preparing, NoAmmo, Reloading
    }
    [SerializeField] protected WeaponState actualState;

   protected virtual void Update()
   {
        Debug.DrawRay(cannonPos.position, cannonPos.forward * weaponRange, Color.yellow);

        switch (actualState) {
            case WeaponState.Reloading:
                timerReloading += Time.deltaTime;
                if(timerReloading >= reloadTime) {
                    actualState = WeaponState.Ready;
                    timerReloading = 0f;
                    actualAmmo = ammoPerMagazine;
                }
                break;
            case WeaponState.Preparing:
                timerShootCooldown += Time.deltaTime;
                if(timerShootCooldown >= shootCooldown) {
                    actualState = WeaponState.Ready;
                    timerShootCooldown = 0f;
                }
                break;
        }
   }

    public virtual void Shoot() {
        if (actualState == WeaponState.Preparing || actualState == WeaponState.Reloading) 
            return;
        

        if(actualState == WeaponState.NoAmmo) {
            Reload();
            return;
        }

        if (actualState == WeaponState.Ready) {
            RaycastHit hit;

            if (Physics.Raycast(cannonPos.position, cannonPos.forward, out hit, weaponRange)) {

                if (layerEnemy == (layerEnemy | (1 << hit.transform.gameObject.layer))) {
                    Enemy e = hit.transform.GetComponentInParent<Enemy>();

                    if (e) {
                        if (hit.transform.CompareTag("EnemyHead")) {
                            Debug.Log("PUM HEADSHOT");
                            e.Hit(damage * headshotMultiplier);
                        }
                        else
                            e.Hit(damage);
                    }

                    GameObject ps = Instantiate(particlesHitPrefab, hit.point, particlesHitPrefab.transform.rotation);
                    Destroy(ps, 1);
                }
                else {
                    GameObject hole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));
                    Destroy(hole, 5f);
                }
            }

            actualState = WeaponState.Preparing;

            actualAmmo--;
            if (actualAmmo <= 0)
                actualState = WeaponState.NoAmmo;
            
        }
    }

    public virtual void Reload() {
        if (actualState != WeaponState.Reloading) {
            actualState = WeaponState.Reloading;
            timerReloading = 0f;
        }
    }





}
