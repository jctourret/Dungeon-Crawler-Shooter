using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

    [SerializeField] protected int buckshots;
    [SerializeField] protected float bulletSpread;

    public override void Shoot() {
        if (actualState == WeaponState.Preparing || actualState == WeaponState.Reloading) {
            Debug.Log("Cant shoot");
            return;
        }

        if (actualState == WeaponState.NoAmmo) {
            Reload();
            return;
        }

        if (actualState == WeaponState.Ready) {

            for (int i = 0; i < buckshots; i++) {
                RaycastHit hit;

                if (Physics.Raycast(cannonPos.position, CalculateBulletSpread(), out hit, weaponRange)) {
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

    Vector3 CalculateBulletSpread() {
        Vector3 targetPos = cannonPos.position + cannonPos.forward * weaponRange;
        targetPos += new Vector3(
            Random.Range(-bulletSpread, bulletSpread),
            Random.Range(-bulletSpread, bulletSpread),
            Random.Range(-bulletSpread, bulletSpread));


        Vector3 dir = targetPos - cannonPos.position;
        return dir.normalized;
    }

}
