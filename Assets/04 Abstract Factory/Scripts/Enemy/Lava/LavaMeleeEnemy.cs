using UnityEngine;

namespace AbstractFactory
{
    public class LavaMeleeEnemy : MeleeEnemy
    {
        protected override void MeleeAttack()
        {
            Debug.Log("Lava Melee Attack");
        }
    }
}
