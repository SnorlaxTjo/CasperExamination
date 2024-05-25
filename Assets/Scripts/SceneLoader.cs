using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Loads the scene with the inputted id
    public void LoadScene(int sceneId)
    {
        int sceneToLoad = LoopBuildIndex(sceneId);
        SceneManager.LoadScene(sceneToLoad);
    }

    //Loads the next scene
    public void LoadNextScene()
    {
        int sceneId = LoopBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(sceneId);
    }

    //Reloads the current scene
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //Quits the game. Yes, Quit is intentionally spelled incorrectly in the Debug.Log, it's an old reference
    public void QuitGame()
    {
        Debug.Log("QUTI GAME");
        Application.Quit();
    }

    //Loops the build index of the given scene-ID to make sure it doesn't go out of the amount of scenes
    int LoopBuildIndex(int sceneId)
    {
        if (sceneId >= SceneManager.sceneCountInBuildSettings)
        {
            return 0;
        }
        else
        {
            return sceneId;
        }
    }
}
