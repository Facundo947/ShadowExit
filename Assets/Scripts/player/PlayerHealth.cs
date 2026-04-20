using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float invulnerabilityTime = 0.5f;
    [SerializeField] private float deathRestartDelay = 1f;

    private int currentHealth;
    private float lastDamageTime = float.NegativeInfinity;
    private bool isDead;
    private Movement movement;
    private Rigidbody2D rb;
    private Collider2D[] colliders;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || isDead)
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
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        Debug.Log("Player murio.", this);

        if (movement != null)
        {
            movement.enabled = false;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        if (colliders != null)
        {
            foreach (Collider2D hitbox in colliders)
            {
                hitbox.enabled = false;
            }
        }

        Invoke(nameof(RestartScene), deathRestartDelay);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
