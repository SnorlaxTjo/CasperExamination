using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] OptionSaver optionSaver;

    AudioSource sfxSource;

    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();

        ChangeSfxVolume();
    }

    // Sets the volume to the selected volume in the options
    public void ChangeSfxVolume()
    {
        float volumeDecimal = optionSaver.sfxVolumePercentage / 100;

        sfxSource.volume = volumeDecimal;
    }

    // Plays the sent audio clip
    public void PlaySound(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }
}
