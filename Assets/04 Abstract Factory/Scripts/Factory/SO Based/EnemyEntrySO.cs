using UnityEngine;

namespace AbstractFactory
{
    [CreateAssetMenu(fileName = "Enemy Entry SO", menuName = "ScriptableObjects/Abstract Factory/Enemy Entry")]
    public class EnemyEntrySO : ScriptableObject
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
        [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    }

    public enum EnemyType { Melee, Ranged, Flying }
}