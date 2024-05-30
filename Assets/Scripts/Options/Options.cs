using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] OptionSaver optionsToSaveTo;

    [Header("Mouse Sensitivity")]
    [SerializeField] Slider mouseSensitivitySlider;
    [SerializeField] TextMeshProUGUI sensitivityDisplayText;

    [Header("Volumes")]
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] TextMeshProUGUI musicVolumeDisplayText;
    [SerializeField] TextMeshProUGUI sfxVolumeDisplayText;

    //Saves the options to the ScriptableObject

    public void SaveOptions()
    {
        optionsToSaveTo.mouseSesnitivity = mouseSensitivitySlider.value;

        optionsToSaveTo.musicVolumePercentage = musicVolumeSlider.value;
        optionsToSaveTo.sfxVolumePercentage = sfxVolumeSlider.value;

        FindObjectOfType<MusicController>().ChangeMusicVolume();
        FindObjectOfType<SFXController>().ChangeSfxVolume();
    }

    //Loads the options currently set in the ScriptableObject
    public void LoadOptions()
    {
        mouseSensitivitySlider.value = optionsToSaveTo.mouseSesnitivity;
        sensitivityDisplayText.text = mouseSensitivitySlider.value.ToString();

        musicVolumeSlider.value = optionsToSaveTo.musicVolumePercentage;
        musicVolumeDisplayText.text = musicVolumeSlider.value.ToString();
        sfxVolumeSlider.value = optionsToSaveTo.sfxVolumePercentage;
        sfxVolumeDisplayText.text = sfxVolumeSlider.value.ToString();
    }

    //Changes the display text of mouse sensitivity setting
    public void ChangeMouseSensitivity()
    {
        sensitivityDisplayText.text = mouseSensitivitySlider.value.ToString();
    }

    public void ChangeMusicVolume()
    {
        musicVolumeDisplayText.text = musicVolumeSlider.value.ToString();
        FindObjectOfType<MusicController>().GetComponent<AudioSource>().volume = musicVolumeSlider.value / 100;
    }

    public void ChangeSfxVolume()
    {
        sfxVolumeDisplayText.text = sfxVolumeSlider.value.ToString();
    }
}
