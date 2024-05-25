using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] AudioClip damageSound;

    int currentHealth;

    PlayerUIController uiController;
    SFXController sfx;

    public int MaxHealth { get { return maxHealth; } }

    private void Start()
    {
        uiController = FindObjectOfType<PlayerUIController>();
        sfx = FindObjectOfType<SFXController>();

        currentHealth = maxHealth;
    }

    //Removes health from the player when hit
    public void Damage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            FindObjectOfType<SceneLoader>().ReloadScene();
        }

        uiController.SetHealthBar(currentHealth);
        sfx.PlaySound(damageSound);
    }
}
