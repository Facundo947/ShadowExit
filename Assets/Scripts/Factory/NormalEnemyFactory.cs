using UnityEngine;
using ShadowExit.EnemyStrategySystem;

[CreateAssetMenu(fileName = "NormalEnemyFactory", menuName = "Factory/Normal Enemy Factory")]
public class NormalEnemyFactory : EnemyFactoryBase
{
    [Header("Prefabs")]
    [SerializeField] private GameObject followEnemyPrefab;
    [SerializeField] private GameObject patrolEnemyPrefab;
    [Header("Movement")]
    [SerializeField] private float followSpeed = 3f;
    [SerializeField] private float followMinDistance = 1f;
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float patrolWaitTime = 1.5f;
    [SerializeField] private float patrolChaseRange = 3f;
    [SerializeField] private float patrolStopDistance = 1f;
    [Header("Health")]
    [SerializeField] private int enemyMaxHealth = 1;
    [Header("Damage")]
    [SerializeField] private int contactDamage = 1;

    public override GameObject CreateFollowEnemy(Vector3 position, Transform followTarget)
    {
        if (followEnemyPrefab == null)
        {
            Debug.LogError("NormalEnemyFactory: falta asignar el prefab del enemigo follow.");
            return null;
        }

        GameObject enemy = Instantiate(followEnemyPrefab, position, Quaternion.identity);

        EnsureContactDamage(enemy);
        EnsureEnemyHealth(enemy);
        ConfigureFollowStrategy(enemy, followTarget);

        return enemy;
    }

    public override GameObject CreatePatrolEnemy(Vector3 position, Transform followTarget)
    {
        if (patrolEnemyPrefab == null)
        {
            Debug.LogError("NormalEnemyFactory: falta asignar el prefab del enemigo patrol.");
            return null;
        }

        GameObject enemy = Instantiate(patrolEnemyPrefab, position, Quaternion.identity);

        EnsureContactDamage(enemy);
        EnsureEnemyHealth(enemy);
        ConfigurePatrolStrategy(enemy, followTarget);

        return enemy;
    }

    private void EnsureContactDamage(GameObject enemy)
    {
        EnemyContactDamage contactDamageComponent = enemy.GetComponent<EnemyContactDamage>();
        if (contactDamageComponent == null)
        {
            contactDamageComponent = enemy.AddComponent<EnemyContactDamage>();
        }

        contactDamageComponent.SetDamage(contactDamage);
    }

    private void EnsureEnemyHealth(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth == null)
        {
            enemyHealth = enemy.AddComponent<EnemyHealth>();
        }

        enemyHealth.SetMaxHealth(enemyMaxHealth);
    }

    private void ConfigureFollowStrategy(GameObject enemy, Transform followTarget)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        EnemyMover mover = enemy.GetComponent<EnemyMover>();

        if (rb == null || mover == null)
        {
            Debug.LogWarning("NormalEnemyFactory: el prefab follow necesita Rigidbody2D y EnemyMover.", enemy);
            return;
        }

        mover.SetStrategy(new FollowMovementStrategy(enemy.transform, rb, followSpeed, followTarget, followMinDistance));
    }

    private void ConfigurePatrolStrategy(GameObject enemy, Transform followTarget)
    {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        EnemyMover mover = enemy.GetComponent<EnemyMover>();

        if (rb == null || mover == null)
        {
            Debug.LogWarning("NormalEnemyFactory: el prefab patrol necesita Rigidbody2D y EnemyMover.", enemy);
            return;
        }

        Transform[] waypoints = CollectChildWaypoints(enemy.transform);
        mover.SetStrategy(new PatrolMovementStrategy(
            enemy.transform,
            rb,
            patrolSpeed,
            followTarget,
            waypoints,
            patrolChaseRange,
            patrolStopDistance,
            patrolWaitTime));
    }

    private Transform[] CollectChildWaypoints(Transform enemyTransform)
    {
        int childCount = enemyTransform.childCount;
        if (childCount == 0)
        {
            return System.Array.Empty<Transform>();
        }

        Transform[] waypoints = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            waypoints[i] = enemyTransform.GetChild(i);
        }

        return waypoints;
    }
}
