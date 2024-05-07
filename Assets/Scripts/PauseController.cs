using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    [SerializeField] UnityEvent gamePaused;
    [SerializeField] UnityEvent gameResumed;

    bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0;
                gamePaused.Invoke();
            }
            else
            {
                Time.timeScale = 1;
                gameResumed.Invoke();
            }
        }
    }
}
