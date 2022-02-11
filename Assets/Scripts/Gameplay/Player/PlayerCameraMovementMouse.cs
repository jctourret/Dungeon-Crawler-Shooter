using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovementMouse : MonoBehaviour
{
    [SerializeField] float mouseSensitivity;
    [SerializeField] Transform playerBody;

    float xRotation = 0;

    public bool canMoveCamera = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!canMoveCamera)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
