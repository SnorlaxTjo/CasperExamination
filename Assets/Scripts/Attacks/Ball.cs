using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float fasterMovementSpeed;

    [Space]
    [SerializeField] float lifetime;
    [SerializeField] string[] ignoreCollisionTags;
    [SerializeField] float timeWithoutCollider;

    Vector3 spawnPosition;
    Vector3 aimPosition;
    Vector3 aimDirection;
    float lifetimeLeft;

    Collider ballCollider;

    private void Start()
    {
        ballCollider = GetComponent<Collider>();

        lifetimeLeft = lifetime;
        StartCoroutine(AddCollider());
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

    IEnumerator AddCollider()
    {
        yield return new WaitForSeconds(timeWithoutCollider);

        ballCollider.enabled = true;
    }

    public void SpeedUp()
    {
        movementSpeed = fasterMovementSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        int collisionIgnores = 0;

        foreach (string tag in ignoreCollisionTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                collisionIgnores++;
            }
        }
        
        if (collisionIgnores <= 0)
        {
            if (other.gameObject.CompareTag("TrainingDummy"))
            {
                other.gameObject.GetComponent<TrainingDummy>().GotHit();
            }

            lifetime = 0;
        }
    }
}
