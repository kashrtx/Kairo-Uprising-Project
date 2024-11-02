using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxArmor = 50;
    private int currentHealth;
    private int currentArmor;

    public Slider healthSlider; // UI Slider for health display (optional)
    public Slider armorSlider; // UI Slider for armor display (optional)
    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;

        if (healthSlider != null)
            healthSlider.maxValue = maxHealth;
        if (armorSlider != null)
            armorSlider.maxValue = maxArmor;

        UpdateHealthUI();
        UpdateArmorUI();
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentArmor > 0)
        {
            // Armor absorbs damage first
            int armorAbsorb = Mathf.Min(damageAmount, currentArmor);
            currentArmor -= armorAbsorb;
            damageAmount -= armorAbsorb;
        }

        if (damageAmount > 0)
        {
            // Remaining damage applies to health
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            animator.SetTrigger("TakeDamage");

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        UpdateHealthUI();
        UpdateArmorUI();
    }

    public void AddArmor(int armorAmount)
    {
        currentArmor += armorAmount;
        currentArmor = Mathf.Clamp(currentArmor, 0, maxArmor);
        UpdateArmorUI();
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        Debug.Log("Player has died.");
        // Add game-over logic here, e.g., disabling player movement
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }

    private void UpdateArmorUI()
    {
        if (armorSlider != null)
            armorSlider.value = currentArmor;
    }
}
