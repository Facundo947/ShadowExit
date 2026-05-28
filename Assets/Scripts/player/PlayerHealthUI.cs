using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour, IPlayerHealthObserver
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider healthSlider;

    private void OnEnable()
    {
        ResolvePlayerHealth();
        Subscribe();
        RefreshHealth();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        if (playerHealth == null)
        {
            return;
        }

        playerHealth.RegisterObserver(this);
    }

    private void Unsubscribe()
    {
        if (playerHealth == null)
        {
            return;
        }

        playerHealth.UnregisterObserver(this);
    }

    private void ResolvePlayerHealth()
    {
        if (playerHealth != null)
        {
            return;
        }

        playerHealth = FindAnyObjectByType<PlayerHealth>();
    }

    public void OnNotify(PlayerHealthNotification notification)
    {
        RefreshHealth(notification.CurrentHealth, notification.MaxHealth);
    }

    private void RefreshHealth()
    {
        if (playerHealth == null)
        {
            RefreshHealth(0, 1);
            return;
        }

        RefreshHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    private void RefreshHealth(int currentHealth, int maxHealth)
    {
        int safeMaxHealth = Mathf.Max(1, maxHealth);

        if (healthText != null)
        {
            healthText.text = $"Vida: {currentHealth}/{safeMaxHealth}";
        }

        if (healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = safeMaxHealth;
            healthSlider.value = currentHealth;
        }
    }
}
