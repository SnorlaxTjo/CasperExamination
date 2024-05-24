using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform[] cameraTransform;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    [Header("Jumping")]
    [SerializeField] float jumpSpeed;
    [SerializeField] float maxJumpTime;
    [SerializeField] float coyoteTime;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundLayerMask;

    Vector3 movementDirection;
    float ySpeed;
    bool isGrounded;
    float coyoteTimeLeft;
    bool isJumping;
    bool canJump;
    float jumpTimeLeft;
    float lastRotation;
    int cameraIndex;
    bool isMoving;

    Rigidbody playerRigidbody;
    Laser laser;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        laser = GetComponentInChildren<Laser>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Jump();

        if (laser.IsInFocusMode)
        {
            cameraIndex = 1;
        }
        else
        {
            cameraIndex = 0;
        }
        RotateCharacter();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        isMoving = Mathf.Abs(horizontalInput) > Mathf.Epsilon || Mathf.Abs(verticalInput) > Mathf.Epsilon;

        movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = movementDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);
        movementDirection = Quaternion.AngleAxis(cameraTransform[cameraIndex].rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        playerRigidbody.velocity = new Vector3(movementDirection.x * magnitude * moveSpeed, ySpeed, movementDirection.z * magnitude * moveSpeed);
    }

    void RotateCharacter()
    {
        if (laser.IsInFocusMode) { return; }

        if (isMoving)
        {
            lastRotation = transform.localEulerAngles.y;

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, lastRotation, 0);
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if (!isGrounded && coyoteTimeLeft > 0)
        {
            coyoteTimeLeft -= Time.deltaTime;

            if (coyoteTimeLeft <= 0)
            {
                canJump = false;
            }
        }

        if (Input.GetButton("Jump") && canJump)
        {
            jumpTimeLeft -= Time.deltaTime;
            isJumping = true;

            ySpeed = jumpSpeed;
            coyoteTimeLeft = 0;

            if (jumpTimeLeft < 0)
            {
                canJump = false;
            }
        }
        else
        {
            isJumping = false;
        }

        if (Input.GetButtonUp("Jump"))
        {
            canJump = false;
        }

        if (!isJumping)
        {
            if (!isGrounded)
            {
                ySpeed += Physics.gravity.y * Time.deltaTime;
            }
            else
            {
                ySpeed = 0;
                coyoteTimeLeft = coyoteTime;
                jumpTimeLeft = maxJumpTime;
                canJump = true;
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
