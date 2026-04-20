    using UnityEngine;

[CreateAssetMenu(fileName = "NormalEnemyFactory", menuName = "Factory/Normal Enemy Factory")]
public class NormalEnemyFactory : EnemyFactoryBase
{
    [Header("Prefabs")]
    [SerializeField] private GameObject followEnemyPrefab;
    [SerializeField] private GameObject patrolEnemyPrefab;

    public override GameObject CreateFollowEnemy(Vector3 position, Transform followTarget)
    {
        if (followEnemyPrefab == null)
        {
            Debug.LogError("NormalEnemyFactory: falta asignar el prefab del enemigo follow.");
            return null;
        }

        GameObject enemy = Instantiate(followEnemyPrefab, position, Quaternion.identity);

        FollowEnemy followEnemy = enemy.GetComponent<FollowEnemy>();
        if (followEnemy != null && followTarget != null)
        {
            followEnemy.SetTarget(followTarget);
        }

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

        PatrollEnemy patrolEnemy = enemy.GetComponent<PatrollEnemy>();
        if (patrolEnemy != null && followTarget != null)
        {
            patrolEnemy.SetTarget(followTarget);
        }

        return enemy;
    }
}
