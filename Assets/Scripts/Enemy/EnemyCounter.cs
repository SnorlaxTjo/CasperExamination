using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] UnityEvent whatToDoAtZeroEnemies;

    int enemiesLeft;

    private void Start()
    {
        enemiesLeft = FindObjectsOfType<EnemyHealth>().Length;
    }

    public void RemoveEnemy()
    {
        enemiesLeft--;

        if (enemiesLeft <= 0)
        {
            whatToDoAtZeroEnemies?.Invoke();
        }
    }
}
