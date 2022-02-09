using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected float damage;

    [SerializeField] protected float shootCooldown;
    protected float timerShootCooldown = 0;

    [SerializeField] protected int ammoPerMagazine;
    [SerializeField] protected int actualAmmo;

    [SerializeField] protected float reloadTime;
    protected float timerReloading = 0;

    [SerializeField] protected LayerMask layerEnemy;


    protected enum WeaponState {
        Ready, Preparing, NoAmmo, Reloading
    }
    [SerializeField] protected WeaponState actualState;

   protected virtual void Update()
   {
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
        if (actualState == WeaponState.Preparing || actualState == WeaponState.Reloading) {
            Debug.Log("Cant shoot");
            return;
        }

        if(actualState == WeaponState.NoAmmo) {
            Reload();
            return;
        }

        if (actualState == WeaponState.Ready) {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10)) {
                if (layerEnemy == (layerEnemy | (1 << hit.transform.gameObject.layer))) {
                    Enemy e = hit.transform.GetComponent<Enemy>();
                    if (e)
                        e.Hit(damage);
                }
            }

            actualState = WeaponState.Preparing;
            Debug.Log("Pew pew");

            actualAmmo--;
            if (actualAmmo <= 0) {
                actualState = WeaponState.NoAmmo;
                Debug.Log("No ammo");
            }
        }
    }

    public virtual void Reload() {
        if (actualState != WeaponState.Reloading) {
            actualState = WeaponState.Reloading;
            timerReloading = 0f;
            Debug.Log("reloading");
        }
    }





}
