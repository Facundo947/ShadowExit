using UnityEngine;

namespace AbstractFactory
{
    public class SnowFlyingEnemy : FlyingEnemy
    {
        protected override void FlyingAttack()
        {
            Debug.Log("Snow Flying Attack");
        }
    }
}
