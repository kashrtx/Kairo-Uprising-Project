using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxArmor = 100;
    private int currentHealth;
    private int currentArmor;

    public TMP_Text healthText; // TextMeshPro for health display
    public TMP_Text armorText; // TextMeshPro for armor display
    public AudioSource painSound; // Audio source for player pain sound
    private string gameOverSceneName = "Game Over";

    private float lastDamageTime;
    private float armorRegenDelay = 10f;
    private float armorRegenRate = 3f; // Time interval for armor regeneration
    private float armorRegenTimer = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;

        UpdateHealthUI();
        UpdateArmorUI();
    }

    private void Update()
    {
        // Auto-regenerate armor after 10 seconds of no damage
        if (Time.time - lastDamageTime >= armorRegenDelay && currentArmor < maxArmor)
        {
            armorRegenTimer += Time.deltaTime;
            if (armorRegenTimer >= armorRegenRate)
            {
                currentArmor = Mathf.Min(currentArmor + 25, maxArmor); // Regenerate 25 armor every 3 seconds
                UpdateArmorUI();
                armorRegenTimer = 0f;
            }
        }
        else
        {
            armorRegenTimer = 0f; // Reset timer if damage was taken recently
        }
    }

    public void TakeDamage(int damageAmount)
    {
        lastDamageTime = Time.time;
        armorRegenTimer = 0f; // Reset armor regen timer when damage is taken

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

            if (painSound != null && !painSound.isPlaying)
            {
                painSound.Play();
            }

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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Player has died.");
        SceneManager.LoadScene(gameOverSceneName);
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = $"Health: {currentHealth}";
    }

    private void UpdateArmorUI()
    {
        if (armorText != null)
            armorText.text = $"Armor: {currentArmor}";
    }
}
