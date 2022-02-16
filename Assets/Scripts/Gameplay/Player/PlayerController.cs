using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float health;
    [SerializeField] Weapon actualWeapon;
    [SerializeField] PlayerCameraMovementMouse playerCameraMouse;

    public enum PlayerState {
        Alive, Dead
    }
    [SerializeField] PlayerState actualState;

    void Update() {
        if (actualState == PlayerState.Dead)
            return;

        if (Input.GetMouseButton(0)) {
            actualWeapon.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            actualWeapon.Reload();
        }
    }

    public void Hit(float value) {
        if (actualState == PlayerState.Dead)
            return;

        health -= value;
        if (health <= 0f) {
            if(playerCameraMouse != null)
            {
                playerCameraMouse.canMoveCamera = false;
            }
            actualState = PlayerState.Dead;
            health = 0f;
        }
    }
}
