using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] float timeBeforeAutoHealing;
    [SerializeField] float timeBetweenEachHeal;
    [SerializeField] AudioClip damageSound;
    [SerializeField] GameObject deadCanvas;
    [SerializeField] GameObject pausedCanvas;

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
        // Checks if the player has been hit within a certain amount of time. If not, the player will heal
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

    // Removes health from the player when hit
    public void Damage(int damage)
    {
        currentHealth -= damage;
        isDamaged = true;
        nextAutoHealingTime = timeBeforeAutoHealing;

        if (currentHealth <= 0)
        {
            deadCanvas.SetActive(true);
            GetComponent<PauseController>().Pause();
            pausedCanvas.SetActive(false);
        }

        uiController.SetHealthBar(currentHealth);
        sfx.PlaySound(damageSound);
    }

    // This heals the player if they haven't been hit for some time
    void Heal()
    {
        currentHealth++;

        uiController.SetHealthBar(currentHealth);
    }
}
