using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float fasterMovementSpeed;

    [Space]
    [SerializeField] float lifetime;
    [SerializeField] float timeWithoutCollider;

    [Space]
    [SerializeField] Material[] ballMaterials;

    Vector3 spawnPosition;
    Vector3 aimPosition;
    Vector3 aimDirection;
    float lifetimeLeft;
    int damage;

    Collider ballCollider;

    public int Damage { set { damage = value; } }

    private void Start()
    {
        ballCollider = GetComponent<Collider>();

        lifetimeLeft = lifetime;

        GetComponent<MeshRenderer>().material = ballMaterials[0];
        StartCoroutine(AddCollider());
    }

    //Gives the ball a spawn position and a direction upon spawning
    public void Init(Vector3 aSpawnPosition, Vector3 anAimPosition)
    {
        spawnPosition = aSpawnPosition;
        aimPosition = new Vector3(anAimPosition.x, aSpawnPosition.y, anAimPosition.z);
        aimDirection = aimPosition - spawnPosition;

        gameObject.transform.position = spawnPosition;
        gameObject.transform.LookAt(aimDirection, transform.up);
    }

    //Where the magic of moving the ball actually happens
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

    //Collider on ball is disabled for a short period of time in the beginning to avoid collision with the object which fired it
    //This adds that collider back after a short period of time
    IEnumerator AddCollider()
    {
        yield return new WaitForSeconds(timeWithoutCollider);

        ballCollider.enabled = true;
    }

    //If you shoot the ball with the laser, the ball go fast
    public void SpeedUp(Vector3 direction)
    {
        movementSpeed = fasterMovementSpeed;
        aimDirection = new Vector3(direction.x, 0, direction.z);
        GetComponent<MeshRenderer>().material = ballMaterials[1];
    }

    //Checks all different possible things to collide with that will actually do something, and does that thing
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("TrainingDummy"))
        {
            other.gameObject.GetComponent<TrainingDummy>().GotHit();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().Damage(damage);
        }
        else if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.SetActive(false);

            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().Damage(damage);
        }

        lifetime = 0;
    }
}
