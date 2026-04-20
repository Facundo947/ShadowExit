using UnityEngine;

namespace AbstractFactory
{
    public class NormalFlyingEnemy : FlyingEnemy
    {
        protected override void FlyingAttack()
        {
            Debug.Log("Flying Normal Attack");
        }
    }

}