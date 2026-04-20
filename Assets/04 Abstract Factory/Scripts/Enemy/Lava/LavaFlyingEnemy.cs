using UnityEngine;

namespace AbstractFactory
{
    public class LavaFlyingEnemy : FlyingEnemy
    {
        protected override void FlyingAttack()
        {
            Debug.Log("Lava Flying Attack");
        }
    }
}
