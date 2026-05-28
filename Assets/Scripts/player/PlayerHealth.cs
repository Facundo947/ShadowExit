using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ShadowExit.PlayerStateSystem;

public class PlayerHealth : MonoBehaviour, IPlayerHealthSubject
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
    private readonly List<IPlayerHealthObserver> observers = new();

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
        EnsurePlayerAttack();
        EnsurePlayerBrain();
        movement = GetComponent<Movement>();
        rb = GetComponent<Rigidbody2D>();
        colliders = GetComponents<Collider2D>();
    }

    public void RegisterObserver(IPlayerHealthObserver observer)
    {
        if (observer == null || observers.Contains(observer))
        {
            return;
        }

        observers.Add(observer);
        observer.OnNotify(CreateNotification(PlayerHealthNotificationType.HealthChanged));
    }

    public void UnregisterObserver(IPlayerHealthObserver observer)
    {
        if (observer == null)
        {
            return;
        }

        observers.Remove(observer);
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
        NotifyObservers(CreateNotification(PlayerHealthNotificationType.HealthChanged));
        NotifyObservers(CreateNotification(PlayerHealthNotificationType.Damaged));

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
        NotifyObservers(CreateNotification(PlayerHealthNotificationType.Died));
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

    private void EnsurePlayerAttack()
    {
        if (GetComponent<PlayerAttack>() == null)
        {
            gameObject.AddComponent<PlayerAttack>();
        }
    }

    private void EnsurePlayerBrain()
    {
        if (GetComponent<PlayerBrain>() == null)
        {
            gameObject.AddComponent<PlayerBrain>();
        }
    }

    public void NotifyObservers(PlayerHealthNotification notification)
    {
        foreach (IPlayerHealthObserver observer in observers)
        {
            observer.OnNotify(notification);
        }
    }

    private PlayerHealthNotification CreateNotification(PlayerHealthNotificationType type)
    {
        return new PlayerHealthNotification(type, currentHealth, maxHealth);
    }
}
