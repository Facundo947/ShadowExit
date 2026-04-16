using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{//hacerlo scriptableobject y abstract factory
    [SerializeField] private EnemyPrefabEntry[] enemyPrefabs;

    private readonly Dictionary<EnemyType, GameObject> enemyTable = new();

    private void Awake()
    {
        BuildEnemyTable();
    }

    private void OnValidate()
    {
        BuildEnemyTable();
    }

    public GameObject CreateEnemy(EnemyType type, Vector3 position)
    {
        if (!enemyTable.TryGetValue(type, out GameObject prefab) || prefab == null)
        {
            Debug.LogError($"EnemyFactory: no hay prefab configurado para el tipo {type}.", this);
            return null;
        }

        return Instantiate(prefab, position, Quaternion.identity);
    }

    private void BuildEnemyTable()
    {
        enemyTable.Clear();

        if (enemyPrefabs == null)
        {
            return;
        }

        foreach (EnemyPrefabEntry entry in enemyPrefabs)
        {
            if (entry.prefab == null)
            {
                Debug.LogWarning($"EnemyFactory: el tipo {entry.type} no tiene prefab asignado.", this);
                continue;
            }

            enemyTable[entry.type] = entry.prefab;
        }
    }
}
//pasarlo a parte como scriptableobject
[Serializable]
public struct EnemyPrefabEntry
{
    public EnemyType type;
    public GameObject prefab;
}