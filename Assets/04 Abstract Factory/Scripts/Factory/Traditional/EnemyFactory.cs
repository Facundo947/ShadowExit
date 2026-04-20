using UnityEngine;

namespace AbstractFactory
{
    public abstract class EnemyFactory : MonoBehaviour
    {
        [SerializeField] protected GameObject[] enemyPrefabs;

        public abstract MeleeEnemy CreateMeleeEnemy();
        public abstract RangedEnemy CreateRangedEnemy();
        public abstract FlyingEnemy CreateFlyingEnemy();
    }
}
