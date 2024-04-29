using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpSpeed;

    Vector3 movementDirection;
    float ySpeed;

    Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        RotateCharacter();  
    }

    void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = movementDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);
        movementDirection.Normalize();

        playerRigidbody.velocity = new Vector3(movementDirection.x * magnitude * moveSpeed, ySpeed, movementDirection.z * magnitude * moveSpeed);
    }

    void RotateCharacter()
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            ySpeed = jumpSpeed;
        }
    }
}
