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

    //Only active when laser-weapon is in focus mode, enabling first-person aiming, and this moves the camera around
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
