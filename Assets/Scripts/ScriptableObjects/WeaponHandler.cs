using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponHandler", menuName = "ScriptableObjects/WeaponHandler", order = 1)]
public class WeaponHandler : ScriptableObject
{
    public int damage;
    public float reloadTime;
}
