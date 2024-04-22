using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    bool moveForwards;
    bool moveBackwards;
    bool moveLeft;
    bool moveRight;
    bool isMoving;
    Vector3 moveDirection;
    
    Rigidbody playerRigidbody;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetDirection();

        playerRigidbody.velocity = new Vector3(moveDirection.x * Vector3.forward.x * moveSpeed, playerRigidbody.velocity.y, moveDirection.y * Vector3.forward.z * moveSpeed);
        Debug.Log(playerRigidbody.velocity);
    }

    void GetDirection()
    {
        isMoving = moveForwards || moveBackwards || moveLeft || moveRight;

        moveForwards = Input.GetKey(KeyCode.W);
        moveBackwards = Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);

        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection.y += 1;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            moveDirection.y -= 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection.y -= 1;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            moveDirection.y += 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection.x -= 1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            moveDirection.x += 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection.x += 1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            moveDirection.x -= 1;
        }
    }
}
