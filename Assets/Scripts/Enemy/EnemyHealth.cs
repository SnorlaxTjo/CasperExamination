using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] int maxHealth;
    [SerializeField] AudioClip damageSound;

    int health;

    EnemyButton spawnButton;
    SFXController sfx;

    public EnemyButton SpawnButton { set { spawnButton = value; } }

    private void Start()
    {
        sfx = FindObjectOfType<SFXController>();

        health = maxHealth;
        healthText.text = health.ToString();
    }

    //Does damage to the enemy upon hitting it
    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            spawnButton.GetComponent<Collider>().enabled = true;
            spawnButton.GetComponent<MeshRenderer>().enabled = true;

            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        healthText.text = health.ToString();
        sfx.PlaySound(damageSound);
    }
}
