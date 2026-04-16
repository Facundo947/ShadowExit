using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnCollisionStay2D(Collision2D collision)
    {
        DealDamage(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        DealDamage(other.gameObject);
    }

    private void DealDamage(GameObject target)
    {
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            return;
        }

        playerHealth.TakeDamage(damage);
    }
}
