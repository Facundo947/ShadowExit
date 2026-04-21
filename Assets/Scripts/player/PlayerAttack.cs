using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRange = 0.9f;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private float attackCooldown = 0.35f;

    private Movement movement;
    private PlayerHealth playerHealth;
    private float lastAttackTime = float.NegativeInfinity;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (!CanAttack())
        {
            return;
        }

        if (!WasAttackPressedThisFrame())
        {
            return;
        }

        PerformAttack();
    }

    private bool CanAttack()
    {
        if (playerHealth != null && playerHealth.IsDead)
        {
            return false;
        }

        return Time.time >= lastAttackTime + attackCooldown;
    }

    private bool WasAttackPressedThisFrame()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            return true;
        }

        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            return true;
        }

        if (Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            return true;
        }

        return false;
    }

    private void PerformAttack()
    {
        lastAttackTime = Time.time;

        Vector2 attackDirection = movement != null ? movement.FacingDirection : Vector2.right;
        Vector2 attackCenter = (Vector2)transform.position + attackDirection * attackRange;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackCenter, attackRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject)
            {
                continue;
            }

            EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
            if (enemyHealth == null)
            {
                enemyHealth = hit.GetComponentInParent<EnemyHealth>();
            }

            if (enemyHealth == null)
            {
                continue;
            }

            enemyHealth.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 direction = Vector2.right;

        if (Application.isPlaying && movement != null)
        {
            direction = movement.FacingDirection;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + direction * attackRange, attackRadius);
    }
}
