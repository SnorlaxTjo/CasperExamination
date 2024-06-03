using System;
using TMPro;
using UnityEngine;
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
    [SerializeField] GameObject[] bigSlots;

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
        // This part moves the sub-weapon menu up and down when pressing and releasing TAB
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

    // Changes the weapon hotbar slot whenever a new weapon has been chosen
    public void ChangeHotbarSlot(int slot)
    {
        for (int i = 0; i < hotBarSlots.Length; i++)
        {
            if (i == slot)
            {
                hotBarSlots[i].color = selectedWeaponHotbarColor;
                if (i > 0) bigSlots[i - 1].SetActive(true);
            }
            else
            {
                hotBarSlots[i].color = notSelectedWeaponHotbarColor;
                if (i > 0) bigSlots[i - 1].SetActive(false);
            }
        }

        if (slot != 0)
        {
            menuMask.localPosition = new Vector3(menuPositions[slot - 1], menuMask.localPosition.y, menuMask.localPosition.z);
            ChangeMenuItemTexts(slot);
        }
    }

    // This starts the opening and closing sequence of the sub-menu
    public void OpenWeaponSubTypeMenu(bool open)
    {
        isMovingMenu = true;
        isOpeningMenu = open;
    }

    // This swaps the weapon in the sub-menu
    public void SwapWeaponSubType(bool down)
    {
        if (down)
        {
            for (int i = 0; i < subWeaponSlots[playerAttacks.CurrentWeapon - 1].info.Length; i++)
            {
                SubWeaponInfo currentInfo = subWeaponSlots[playerAttacks.CurrentWeapon - 1].info[i];
                RectTransform currentSlotRectTransform = currentInfo.slot.GetComponent<RectTransform>();

                currentSlotRectTransform.localPosition += new Vector3(0, nextUpDistance, 0);

                if (i == playerAttacks.CurrentWeaponSubType[playerAttacks.CurrentWeapon])
                {
                    currentSlotRectTransform.localScale = Vector3.one;
                    itemTitle.text = currentInfo.name;
                    damage.text = "Damage: " + currentInfo.damage;
                    reloadTime.text = "Reload Time: " + currentInfo.reloadTime.ToString();
                    currentInfo.hotbarImage.enabled = true;
                }
                else
                {
                    currentSlotRectTransform.localScale = new Vector3(nextUpScale, nextUpScale, 1);
                    currentInfo.hotbarImage.enabled = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < subWeaponSlots[playerAttacks.CurrentWeapon - 1].info.Length; i++)
            {
                SubWeaponInfo currentInfo = subWeaponSlots[playerAttacks.CurrentWeapon - 1].info[i];
                RectTransform currentSlotRectTransform = currentInfo.slot.GetComponent<RectTransform>();

                currentSlotRectTransform.localPosition -= new Vector3(0, nextUpDistance, 0);

                if (i == playerAttacks.CurrentWeaponSubType[playerAttacks.CurrentWeapon])
                {
                    currentSlotRectTransform.localScale = Vector3.one;
                    itemTitle.text = currentInfo.name;
                    damage.text = "Damage: " + currentInfo.damage;
                    reloadTime.text = "Reload Time: " + currentInfo.reloadTime.ToString();
                    currentInfo.hotbarImage.enabled = true;
                }
                else
                {
                    currentSlotRectTransform.localScale = new Vector3(nextUpScale, nextUpScale, 1);
                    currentInfo.hotbarImage.enabled = false;
                }
            }
        }
    }

    // This changes the information text objects in the sub-menu
    void ChangeMenuItemTexts(int category)
    {
        itemTitle.text = subWeaponSlots[category - 1].info[playerAttacks.CurrentWeaponSubType[category]].name;
        damage.text = "Damage: " + subWeaponSlots[category - 1].info[playerAttacks.CurrentWeaponSubType[category]].damage;
        reloadTime.text = "Reload Time: " + subWeaponSlots[category - 1].info[playerAttacks.CurrentWeaponSubType[category]].reloadTime;
    }

    // Changes the health bar to the player's current health when hit
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
