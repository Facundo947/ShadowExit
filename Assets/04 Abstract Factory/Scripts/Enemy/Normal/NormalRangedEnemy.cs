using UnityEngine;

namespace AbstractFactory
{
    public class NormalRangedEnemy : RangedEnemy
    {
        protected override void RangedAttack()
        {
            Debug.Log("Normal Ranged Attack");
        }
    }
}