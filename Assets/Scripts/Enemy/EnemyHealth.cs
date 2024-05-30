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
    EnemyCounter counter;

    public EnemyButton SpawnButton { set { spawnButton = value; } }

    private void Start()
    {
        sfx = FindObjectOfType<SFXController>();
        counter = FindObjectOfType<EnemyCounter>();

        health = maxHealth;
        healthText.text = health.ToString();
    }

    //Does damage to the enemy upon hitting it
    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (spawnButton  != null)
            {
                spawnButton.GetComponent<Collider>().enabled = true;
                spawnButton.GetComponent<MeshRenderer>().enabled = true;
            }

            counter.RemoveEnemy();

            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        healthText.text = health.ToString();
        sfx.PlaySound(damageSound);
    }
}
