using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButton : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector2 movementDirections;
    [SerializeField] float moveTime;
    [SerializeField] float waitTime;

    //Spawns in an enemy upon shooting the button with a laser
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
