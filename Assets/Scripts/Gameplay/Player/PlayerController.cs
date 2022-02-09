using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float health;
    [SerializeField] Weapon actualWeapon;

    void Start() {

    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            actualWeapon.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            actualWeapon.Reload();
        }

    }
}
