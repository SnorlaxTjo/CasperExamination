using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] float movementSpeed;

    [Space]
    [SerializeField] float lifetime;

    Vector3 spawnPosition;
    Vector3 aimPosition;
    Vector3 aimDirection;
    float lifetimeLeft;

    private void Start()
    {
        lifetimeLeft = lifetime;
    }

    public void Init(Vector3 aSpawnPosition, Vector3 anAimPosition)
    {
        spawnPosition = aSpawnPosition;
        aimPosition = anAimPosition;
        aimDirection = aimPosition - spawnPosition;

        gameObject.transform.position = spawnPosition;
        gameObject.transform.LookAt(aimDirection, transform.up);
    }

    private void Update()
    {
        transform.position += movementSpeed * Time.deltaTime * aimDirection.normalized;

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            lifetime = 0;
        }
    }
}
