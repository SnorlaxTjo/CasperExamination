using UnityEngine;

/// <summary>
/// Holds information you can change from every weapon whenever
/// </summary>

[CreateAssetMenu(fileName = "WeaponHandler", menuName = "ScriptableObjects/WeaponHandler", order = 1)]
public class WeaponHandler : ScriptableObject
{
    public int damage;
    public float reloadTime;
}
