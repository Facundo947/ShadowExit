using UnityEngine;

namespace AbstractFactory
{
    public class SnowEnemyFactory : EnemyFactory
    {
        public override MeleeEnemy CreateMeleeEnemy()
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[0]);
            return newEnemy.GetComponent<MeleeEnemy>();
        }
        public override RangedEnemy CreateRangedEnemy()
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[1]);
            return newEnemy.GetComponent<RangedEnemy>();
        }
        public override FlyingEnemy CreateFlyingEnemy()
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[2]);
            return newEnemy.GetComponent<FlyingEnemy>();
        }
    }
}
