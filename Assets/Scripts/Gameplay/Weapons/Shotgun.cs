using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon {

    [SerializeField] protected int buckshots;
    [SerializeField] protected float verticalSpread;
    [SerializeField] protected float horizontalSpread;

    protected override void Update() {
        base.Update();
    }

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

                Vector3 spread = new Vector3(Random.Range(-horizontalSpread, horizontalSpread), Random.Range(-verticalSpread, verticalSpread), 0);
                RaycastHit hit;

                if (Physics.Raycast(cannonPos.position, cannonPos.forward + spread, out hit, weaponRange)) {
                    if (layerEnemy == (layerEnemy | (1 << hit.transform.gameObject.layer))) {
                        Enemy e = hit.transform.GetComponent<Enemy>();
                        if (e)
                            e.Hit(damage);
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

}
