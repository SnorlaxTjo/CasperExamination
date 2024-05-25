using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("Hotbar")]
    [SerializeField] Image[] hotBarSlots;
    [SerializeField] Color selectedWeaponHotbarColor;
    [SerializeField] Color notSelectedWeaponHotbarColor;

    [Header("Health")]
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI healthText;

    float maxHealthBarLength;

    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();

        maxHealthBarLength = healthBar.rectTransform.rect.width;
        SetHealthBar(playerHealth.MaxHealth);
        ChangeHotbarSlot(0);
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
