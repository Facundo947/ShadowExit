using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float invulnerabilityTime = 0.5f;

    private int currentHealth;
    private float lastDamageTime = float.NegativeInfinity;

    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            return;
        }

        if (Time.time < lastDamageTime + invulnerabilityTime)
        {
            return;
        }

        currentHealth = Mathf.Max(0, currentHealth - damage);
        lastDamageTime = Time.time;

        Debug.Log($"Player recibio {damage} de dano. Vida restante: {currentHealth}", this);

        if (currentHealth == 0)
        {
            Debug.Log("Player murio.", this);
        }
    }
}
