using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactoryBase enemyFactory;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int spawnCount = 1;
    [SerializeField] private Transform followTargetOverride;

    private Transform followTarget;

    private void Awake()
    {
        followTarget = ResolveFollowTarget();
    }

    private void Start()
    {
        if (enemyFactory == null)
        {
            Debug.LogError("EnemySpawner: falta asignar una EnemyFactoryBase.", this);
            return;
        }

        if (spawnCount <= 0)
        {
            return;
        }

        Transform[] validSpawnPoints = GetValidSpawnPoints();

        if (validSpawnPoints.Length == 0)
        {
            SpawnEnemyAt(transform.position);
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Transform spawnPoint = validSpawnPoints[i % validSpawnPoints.Length];
            SpawnEnemyAt(spawnPoint.position);
        }
    }

    private Transform[] GetValidSpawnPoints()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            return System.Array.Empty<Transform>();
        }

        int validCount = 0;
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint != null)
            {
                validCount++;
            }
        }

        if (validCount == 0)
        {
            return System.Array.Empty<Transform>();
        }

        Transform[] validSpawnPoints = new Transform[validCount];
        int index = 0;
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint == null)
            {
                continue;
            }

            validSpawnPoints[index] = spawnPoint;
            index++;
        }

        return validSpawnPoints;
    }

    private void SpawnEnemyAt(Vector3 spawnPosition)
    {
        GameObject enemy = CreateEnemy(spawnPosition);
        if (enemy == null)
        {
            return;
        }
    }

    private GameObject CreateEnemy(Vector3 spawnPosition)
    {
        switch (enemyType)
        {
            case EnemyType.Follow:
                return enemyFactory.CreateFollowEnemy(spawnPosition, followTarget);
            case EnemyType.Patrol:
                return enemyFactory.CreatePatrolEnemy(spawnPosition, followTarget);
            default:
                Debug.LogError($"EnemySpawner: tipo de enemigo no soportado ({enemyType}).", this);
                return null;
        }
    }

    private Transform ResolveFollowTarget()
    {
        if (followTargetOverride != null)
        {
            return followTargetOverride;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform;
        }

        PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (playerHealth != null)
        {
            return playerHealth.transform;
        }

        Debug.LogWarning("EnemySpawner: no se encontro un objetivo para seguir. Usa el tag Player o asigna un override.", this);
        return null;
    }
}
