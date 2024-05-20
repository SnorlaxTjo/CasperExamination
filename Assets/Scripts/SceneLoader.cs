using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneId)
    {
        int sceneToLoad = LoopBuildIndex(sceneId);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadNextScene()
    {
        int sceneId = LoopBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(sceneId);
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("QUTI GAME");
        Application.Quit();
    }

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
