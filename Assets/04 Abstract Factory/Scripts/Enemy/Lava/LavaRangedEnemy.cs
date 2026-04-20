using UnityEngine;

namespace AbstractFactory
{
    public class LavaRangedEnemy : RangedEnemy
    {
        protected override void RangedAttack()
        {
            Debug.Log("Lava Ranged Attack");
        }
    }
}
