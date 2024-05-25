using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackTypes
{
    nothing,
    slash,
    laser,
    ball,
    total
}
public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] UnityEvent[] attacks;
    [SerializeField] float mouseAxisWeaponSwapBreakpoint;

    int currentWeapon;
    float mouseAxisDelta;

    PlayerUIController playerUIController;

    public int CurrentWeapon { get { return currentWeapon; } }

    private void Start()
    {
        playerUIController = FindObjectOfType<PlayerUIController>();
    }

    private void Update()
    {
        HandleWeaponSwap();

        //Triggers a unity-event calling for the selected weapon's function to fire upon left-click
        if (Input.GetMouseButtonDown(0))
        {
            if (currentWeapon != (int)AttackTypes.nothing)
            {
                attacks[currentWeapon].Invoke();
            }
        }
    }

    //Swaps the weapon upon scrolling
    void HandleWeaponSwap()
    {
        mouseAxisDelta -= Input.mouseScrollDelta.y;
        if (Mathf.Abs(mouseAxisDelta) > mouseAxisWeaponSwapBreakpoint)
        {
            int swapDirectionNumber = (int)Mathf.Sign(mouseAxisDelta);
            mouseAxisDelta -= swapDirectionNumber * mouseAxisWeaponSwapBreakpoint;

            int currentWeaponIndex = (int)currentWeapon + swapDirectionNumber;
            if (currentWeaponIndex >= (int)AttackTypes.total)
            {
                currentWeaponIndex = 0;
            }
            else if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = (int)AttackTypes.total - 1;
            }

            WeaponSwapAnimation(currentWeaponIndex);
        }
    }

    private void WeaponSwapAnimation(int currentWeaponIndex)
    {
        currentWeapon = currentWeaponIndex;
        playerUIController.ChangeHotbarSlot(currentWeapon);
        mouseAxisDelta = 0;
    }
}
