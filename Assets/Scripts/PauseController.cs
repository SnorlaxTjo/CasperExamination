using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    [SerializeField] UnityEvent gamePaused;
    [SerializeField] UnityEvent gameResumed;

    bool isPaused;

    //Controls the pauses if you press ESC

    private void Start()
    {
        Unpause();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                gamePaused.Invoke();
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                gameResumed.Invoke();
            }
        }
    }

    public void Unpause()
    {
        isPaused = false;

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        gameResumed.Invoke();
    }
}
