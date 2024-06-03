using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] CursorLockMode cursorLockOnStart = CursorLockMode.Locked;

    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;
    int selectedResolution;
    List<Resolution> selectedResolutionsList = new List<Resolution>();

    [System.Obsolete]
    private void Start()
    {
        Cursor.lockState = cursorLockOnStart;

        if (fullScreenToggle != null)
        {
            fullScreenToggle.isOn = Screen.fullScreen;
        }

        if (resolutionDropdown != null)
        {
            resolutions = Screen.resolutions;

            List<string> resolutionStringList = new List<string>();
            string newRes;
            foreach (Resolution res in resolutions)
            {
                if (res.height * 16 / 9 == res.width)
                {
                    newRes = res.width + " x " + res.height;
                    if (!resolutionStringList.Contains(newRes))
                    {
                        resolutionStringList.Add(newRes);
                        selectedResolutionsList.Add(res);
                    }
                }                              
            }

            resolutionDropdown.AddOptions(resolutionStringList);
        }
    }

    // Loads the scene with the inputted id
    public void LoadScene(int sceneId)
    {
        int sceneToLoad = LoopBuildIndex(sceneId);
        SceneManager.LoadScene(sceneToLoad);
    }

    // Loads the next scene
    public void LoadNextScene()
    {
        int sceneId = LoopBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(sceneId);
    }

    // Reloads the current scene
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Quits the game. Yes, Quit is intentionally spelled incorrectly in the Debug.Log, it's an old reference
    public void QuitGame()
    {
        Debug.Log("QUTI GAME");
        Application.Quit();
    }

    // Loops the build index of the given scene-ID to make sure it doesn't go out of the amount of scenes
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

    // Sets the screen to fullscreen or not fullscreen. I thought that was quite obvious given the name of the function
    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    // Sets the screen resolution to whatever was selected in the menu
    public void SetResolution()
    {
        selectedResolution = resolutionDropdown.value;
        Screen.SetResolution(selectedResolutionsList[selectedResolution].width, selectedResolutionsList[selectedResolution].height, Screen.fullScreen);
    }
}
