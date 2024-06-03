using UnityEngine;
using UnityEngine.Events;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] UnityEvent whatToDoAtZeroEnemies;

    int enemiesLeft;

    // Counts the amount of enemies in the scene at start
    private void Start()
    {
        enemiesLeft = FindObjectsOfType<EnemyHealth>().Length;
    }

    // This is called when an enemy dies. It then removes it from the counter. If it reaches 0, it does something, like load the next scene
    public void RemoveEnemy()
    {
        enemiesLeft--;

        if (enemiesLeft <= 0)
        {
            whatToDoAtZeroEnemies?.Invoke();
        }
    }
}
