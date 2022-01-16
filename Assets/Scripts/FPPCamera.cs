using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 100f;

    [SerializeField]
    private Transform playerBody = null;

    private float rotationX;
    public float Direction { get; set; } = 1;

    protected void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    protected void Update()
    {
        float mouseX = (Input.GetAxis("Mouse X") * mouseSensitivity) * Direction * Time.deltaTime;
        float mouseY = (Input.GetAxis("Mouse Y") * mouseSensitivity) * Direction * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
