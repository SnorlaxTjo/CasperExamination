using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] OptionSaver options;
    [SerializeField] Transform playerBody;

    float xRotation;

    Laser laser;

    private void Start()
    {
        laser = FindObjectOfType<Laser>();
    }

    private void Update()
    {
        if (!laser.IsInFocusMode) { return; }

        float mouseX = Input.GetAxis("Mouse X") * options.mouseSesnitivity;
        float mouseY = Input.GetAxis("Mouse Y") * options.mouseSesnitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        if (Mathf.Abs(mouseX) > Mathf.Epsilon || Mathf.Abs(mouseY) > Mathf.Epsilon)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
