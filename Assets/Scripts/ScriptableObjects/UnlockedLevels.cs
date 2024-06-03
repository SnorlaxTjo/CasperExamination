using UnityEngine;

/// <summary>
/// This is used to set the unlocked levels in the menu and setting them to unlocked when reaching them
/// </summary>

[CreateAssetMenu(fileName = "UnlockedLevels", menuName = "ScriptableObjects/UnlockedLevels", order = 1)]
public class UnlockedLevels : ScriptableObject
{
    public bool[] unlockedLevels = new bool[3];
}
