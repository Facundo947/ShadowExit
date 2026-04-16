using UnityEngine;
using UnityEngine.Serialization;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float minDistance = 1f;
    [FormerlySerializedAs("Player")]
    [SerializeField] private Transform player;

    public void SetTarget(Transform target)
    {
        player = target;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (Vector2.Distance(transform.position, player.position) > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            return;
        }

        Debug.Log("Ataque");
    }
}
