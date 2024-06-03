using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockedLevels", menuName = "ScriptableObjects/UnlockedLevels", order = 1)]
public class UnlockedLevels : ScriptableObject
{
    public bool[] unlockedLevels = new bool[3];
}
