using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] float timeBeforeAutoHealing;
    [SerializeField] float timeBetweenEachHeal;
    [SerializeField] AudioClip damageSound;

    int currentHealth;
    float nextAutoHealingTime;
    bool isDamaged;

    PlayerUIController uiController;
    SFXController sfx;

    public int MaxHealth { get { return maxHealth; } }

    private void Start()
    {
        uiController = FindObjectOfType<PlayerUIController>();
        sfx = FindObjectOfType<SFXController>();

        currentHealth = maxHealth;
    }

    private void Update()
    {
        nextAutoHealingTime -= Time.deltaTime;

        if (nextAutoHealingTime <= 0)
        {
            nextAutoHealingTime += timeBetweenEachHeal;

            if (isDamaged)
            {               
                isDamaged = false;
            }
            else
            {
                if (currentHealth < maxHealth)
                {
                    Heal();
                }         
            }
            
        }
    }

    //Removes health from the player when hit
    public void Damage(int damage)
    {
        currentHealth -= damage;
        isDamaged = true;
        nextAutoHealingTime = timeBeforeAutoHealing;

        if (currentHealth <= 0)
        {
            FindObjectOfType<SceneLoader>().ReloadScene();
        }

        uiController.SetHealthBar(currentHealth);
        sfx.PlaySound(damageSound);
    }

    void Heal()
    {
        currentHealth++;

        uiController.SetHealthBar(currentHealth);
    }
}
