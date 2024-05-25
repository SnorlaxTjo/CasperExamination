using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OptionSaver", menuName = "ScriptableObjects/OptionSaver", order = 1)]
public class OptionSaver : ScriptableObject
{
    public float mouseSesnitivity;

    public float musicVolumePercentage;
    public float sfxVolumePercentage;
}
