using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int spawnCount = 1;
    [SerializeField] private Transform followTarget;

    private void Start()
    {
        if (enemyFactory == null)
        {
            Debug.LogError("EnemySpawner: falta asignar un EnemyFactory.", this);
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
        GameObject enemy = enemyFactory.CreateEnemy(enemyType, spawnPosition);
        if (enemy == null)
        {
            return;
        }

        FollowEnemy followEnemy = enemy.GetComponent<FollowEnemy>();
        if (followEnemy != null && followTarget != null)
        {
            followEnemy.SetTarget(followTarget);
        }

        PatrollEnemy patrollEnemy = enemy.GetComponent<PatrollEnemy>();
        if (patrollEnemy != null && followTarget != null)
        {
            patrollEnemy.SetTarget(followTarget);
        }
    }
}
