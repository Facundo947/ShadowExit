using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float minDistance = 1f;
    [FormerlySerializedAs("Player")]
    [SerializeField] private Transform player;

    private Rigidbody2D rb;
    private Vector2 targetPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        if (Vector2.Distance(transform.position, player.position) > minDistance)
        {
            targetPosition = Vector2.MoveTowards(rb.position, player.position, speed * Time.fixedDeltaTime);
            rb.MovePosition(targetPosition);
            return;
        }

        Debug.Log("Ataque");
    }
}
