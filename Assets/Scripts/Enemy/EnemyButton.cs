using UnityEngine;

/// <summary>
/// This script was only used in the test scene I used to create new features.
/// It is unused in the actual game, but I don't know if something will break if I remove the script, and I am too scared to try
/// </summary>

public class EnemyButton : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector2 movementDirections;
    [SerializeField] float moveTime;
    [SerializeField] float waitTime;

    // Spawns in an enemy upon shooting the button with a laser
    public void Hit()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        GameObject enemySpawned = Instantiate(enemy);
        enemySpawned.transform.position = spawnPosition;
        enemySpawned.GetComponent<EnemyHealth>().SpawnButton = this;
        EnemyMovement mover = enemySpawned.GetComponent<EnemyMovement>();
        mover.DistancesToMove = movementDirections;
        mover.TimeToMove = moveTime;
        mover.TimeToWait = waitTime;
    }
}
