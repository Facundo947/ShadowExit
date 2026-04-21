using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrollEnemy : MonoBehaviour
{
   [FormerlySerializedAs("Speed")]
   [SerializeField] private float speed = 3f;
   [FormerlySerializedAs("WiteTime")]
   [SerializeField] private float waitTime = 1.5f;
   [SerializeField] private float chaseRange = 3f;
   [SerializeField] private float stopDistance = 1f;
   [SerializeField] private Transform[] waypoints;
   [SerializeField] private Transform player;


    private int currentWaypoint;
    private bool isWaiting;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }

    private void FixedUpdate()
    {
        if (ShouldChasePlayer())
        {
            ChasePlayer();
            return;
        }

        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }

        if (transform.position != waypoints[currentWaypoint].position)
        {
            Vector2 nextPosition = Vector2.MoveTowards(rb.position, waypoints[currentWaypoint].position, speed * Time.fixedDeltaTime);
            rb.MovePosition(nextPosition);
        }
        else if (!isWaiting)
        {
            StartCoroutine(Wait());
        }
    }

    private bool ShouldChasePlayer()
    {
        if (player == null)
        {
            return false;
        }

        return Vector2.Distance(transform.position, player.position) <= chaseRange;
    }

    private void ChasePlayer()
    {
        if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            return;
        }

        Vector2 nextPosition = Vector2.MoveTowards(rb.position, player.position, speed * Time.fixedDeltaTime);
        rb.MovePosition(nextPosition);
    }

    private IEnumerator Wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        currentWaypoint++;

        if (currentWaypoint == waypoints.Length)
        {
            currentWaypoint = 0;
        }

        isWaiting = false;
    }
}
