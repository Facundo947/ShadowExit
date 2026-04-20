using UnityEngine;

namespace AbstractFactory
{
    public class SnowMeleeEnemy : MeleeEnemy
    {
        protected override void MeleeAttack()
        {
            Debug.Log("Snow Melee Attack");
        }
    }
}
