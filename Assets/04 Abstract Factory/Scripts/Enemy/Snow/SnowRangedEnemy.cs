using UnityEngine;

namespace AbstractFactory
{
    public class SnowRangedEnemy : RangedEnemy
    {
        protected override void RangedAttack()
        {
            Debug.Log("Snow Ranged Attack");
        }
    }
}
