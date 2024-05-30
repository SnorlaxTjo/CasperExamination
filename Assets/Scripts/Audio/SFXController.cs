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

    public void ChangeSfxVolume()
    {
        float volumeDecimal = optionSaver.sfxVolumePercentage / 100;

        sfxSource.volume = volumeDecimal;
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }
}
