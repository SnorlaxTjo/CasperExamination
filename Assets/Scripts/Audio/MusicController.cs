using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] OptionSaver optionSaver;

    AudioSource musicSource;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();

        ChangeMusicVolume();
    }

    // Sets the volume of the music to the selected volume in the options
    public void ChangeMusicVolume()
    {
        float volumeDecimal = optionSaver.musicVolumePercentage / 100;

        musicSource.volume = volumeDecimal;
    }
}
