using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("Hotbar")]
    [SerializeField] Image[] hotBarSlots;
    [SerializeField] Color selectedWeaponHotbarColor;
    [SerializeField] Color notSelectedWeaponHotbarColor;

    private void Start()
    {
        ChangeHotbarSlot(0);
    }

    public void ChangeHotbarSlot(int slot)
    {
        for (int i = 0; i < hotBarSlots.Length; i++)
        {
            if (i == slot)
            {
                hotBarSlots[i].color = selectedWeaponHotbarColor;
            }
            else
            {
                hotBarSlots[i].color = notSelectedWeaponHotbarColor;
            }
        }
    }
}
