using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("Hotbar")]
    [SerializeField] Image[] hotBarSlots;
    [SerializeField] Color selectedWeaponHotbarColor;
    [SerializeField] Color notSelectedWeaponHotbarColor;

    [Header("Weapon Sub Types")]
    [SerializeField] RectTransform menu;
    [SerializeField] RectTransform menuMask;
    [SerializeField] float[] menuPositions;
    [SerializeField] SubWeapon[] subWeaponSlots;
    [SerializeField] float timeToMoveMenu;
    [SerializeField] TextMeshProUGUI itemTitle;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI reloadTime;
    [SerializeField] float nextUpScale;
    [SerializeField] float nextUpDistance;

    [Header("Health")]
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI healthText;

    float maxHealthBarLength;
    float subWeaponMenuStartPosY;
    bool isMovingMenu;
    bool isOpeningMenu;
    float menuMovingTime;

    PlayerHealth playerHealth;
    PlayerAttacks playerAttacks;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerAttacks = FindObjectOfType<PlayerAttacks>();

        maxHealthBarLength = healthBar.rectTransform.rect.width;
        subWeaponMenuStartPosY = menu.localPosition.y;

        SetHealthBar(playerHealth.MaxHealth);
        ChangeHotbarSlot(0);
    }

    private void Update()
    {
        if (isMovingMenu)
        {
            if (isOpeningMenu)
            {
                menu.localPosition -= new Vector3(0, subWeaponMenuStartPosY / timeToMoveMenu * Time.deltaTime, 0);
                menuMovingTime += Time.deltaTime;

                if (menuMovingTime >= timeToMoveMenu)
                {
                    menu.localPosition = Vector3.zero;
                    menuMovingTime = timeToMoveMenu;
                    isMovingMenu = false;
                }
            }
            else if (!isOpeningMenu)
            {
                menu.localPosition += new Vector3(0, subWeaponMenuStartPosY / timeToMoveMenu * Time.deltaTime, 0);
                menuMovingTime -= Time.deltaTime;

                if (menuMovingTime <= 0)
                {
                    menu.localPosition = new Vector3(0, subWeaponMenuStartPosY, 0);
                    menuMovingTime = 0;
                    isMovingMenu = false;
                }
            }
        }
    }

    //Changes the weapon hotbar slot whenever a new weapon has been chosen
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

        if (slot != 0)
        {
            menuMask.localPosition = new Vector3(menuPositions[slot - 1], menuMask.localPosition.y, menuMask.localPosition.z);
            ChangeMenuItemTexts(slot);
        }
    }

    public void OpenWeaponSubTypeMenu(bool open)
    {
        isMovingMenu = true;
        isOpeningMenu = open;
    }

    public void SwapWeaponSubType(bool down)
    {
        if (down)
        {
            foreach (SubWeaponInfo weaponInfo in subWeaponSlots[playerAttacks.CurrentWeapon].info)
            {
                weaponInfo.slot.GetComponent<RectTransform>().localPosition += new Vector3(0, nextUpDistance, 0);
            }
        }
    }

    void ChangeMenuItemTexts(int category)
    {
        itemTitle.text = subWeaponSlots[category - 1].info[playerAttacks.CurrentWeaponSubType[category]].name;
        damage.text = "Damage: " + subWeaponSlots[category - 1].info[playerAttacks.CurrentWeaponSubType[category]].damage;
        reloadTime.text = "Reload Time: " + subWeaponSlots[category - 1].info[playerAttacks.CurrentWeaponSubType[category]].reloadTime;
    }

    //Changes the health bar to the player's current health when hit
    public void SetHealthBar(int health)
    {
        float currentHealthPercentage = (float)health / (float)playerHealth.MaxHealth; //These needed to be casted as floats. Even though it says it's not needed, trust me when I say it is

        float healthBarWidth = currentHealthPercentage * maxHealthBarLength;
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarWidth, healthBar.rectTransform.rect.height);

        healthText.text = "HP: " + health + "/" + playerHealth.MaxHealth;
    }
}

[Serializable]
public struct SubWeapon
{
    public SubWeaponInfo[] info;
}

[Serializable]
public struct SubWeaponInfo
{
    public string name;
    public int damage;
    public float reloadTime;
    public GameObject slot;
    public Image hotbarImage;
}
