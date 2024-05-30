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

    public void ChangeMusicVolume()
    {
        float volumeDecimal = optionSaver.musicVolumePercentage / 100;

        musicSource.volume = volumeDecimal;
    }
}
