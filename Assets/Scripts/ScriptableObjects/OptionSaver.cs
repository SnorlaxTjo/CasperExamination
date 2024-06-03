using UnityEngine;

/// <summary>
/// This saves the options that need to be saved and distributed throughout the game in an object
/// </summary>

[CreateAssetMenu(fileName = "OptionSaver", menuName = "ScriptableObjects/OptionSaver", order = 1)]
public class OptionSaver : ScriptableObject
{
    public float mouseSesnitivity;

    public float musicVolumePercentage;
    public float sfxVolumePercentage;
}
