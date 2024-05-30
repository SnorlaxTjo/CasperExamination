using System;
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
    [SerializeField] Attacks[] attacks;
    [SerializeField] float mouseAxisWeaponSwapBreakpoint;

    int currentWeapon;
    int[] currentWeaponSubType = new int[(int)AttackTypes.total];
    float mouseAxisDelta;
    bool isSwappingInCurrentWeapon;

    PlayerUIController playerUIController;

    public int CurrentWeapon { get { return currentWeapon; } }
    public int[] CurrentWeaponSubType { get { return currentWeaponSubType; } }

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
                attacks[currentWeapon].attacks[currentWeaponSubType[currentWeapon]].Invoke();
            }
        }

        if (currentWeapon != 0)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isSwappingInCurrentWeapon = true;
                playerUIController.OpenWeaponSubTypeMenu(true);
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                isSwappingInCurrentWeapon = false;
                playerUIController.OpenWeaponSubTypeMenu(false);
            }
        }
        else
        {
            isSwappingInCurrentWeapon = false;
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

            int currentWeaponIndex;
            
            if (!isSwappingInCurrentWeapon)
            {
                currentWeaponIndex = currentWeapon + swapDirectionNumber;

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
            else
            {
                currentWeaponIndex = currentWeaponSubType[currentWeapon] + swapDirectionNumber;

                if (currentWeaponIndex >= attacks[currentWeapon].attacks.Length)
                {
                    currentWeaponIndex = attacks[currentWeapon].attacks.Length - 1;
                    mouseAxisDelta = 0;
                }
                else if (currentWeaponIndex < 0)
                {
                    currentWeaponIndex = 0;
                    mouseAxisDelta = 0;
                }
                else
                {
                    WeaponSwapAnimation(currentWeaponIndex);
                }
            }
        }
    }

    private void WeaponSwapAnimation(int currentWeaponIndex)
    {
        if (!isSwappingInCurrentWeapon)
        {
            currentWeapon = currentWeaponIndex;
            playerUIController.ChangeHotbarSlot(currentWeapon);
        }
        else
        {
            currentWeaponSubType[currentWeapon] = currentWeaponIndex;
            playerUIController.SwapWeaponSubType(Mathf.Sign(mouseAxisDelta) > 0);
        }

        mouseAxisDelta = 0;
    }
}

[Serializable]
public struct Attacks
{
    public UnityEvent[] attacks;
}
