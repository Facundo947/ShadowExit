using UnityEngine;
using ShadowExit.EnemyStrategySystem;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    private EnemyMovementStrategy currentStrategy;

    private void FixedUpdate()
    {
        currentStrategy?.Move(Time.fixedDeltaTime);
    }

    public void SetStrategy(EnemyMovementStrategy strategy)
    {
        currentStrategy = strategy;
        Debug.Log($"[EnemyMover] cambio estategia {strategy?.GetType().Name ?? "None"}", this);
    }
}
