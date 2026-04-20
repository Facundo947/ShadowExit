using UnityEngine;

namespace AbstractFactory
{
    public class NormalMeleeEnemy : MeleeEnemy
    {
        protected override void MeleeAttack()
        {
            Debug.Log("Normal Melee Attack");
        }
    }
}

