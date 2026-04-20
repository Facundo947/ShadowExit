using UnityEngine;

namespace AbstractFactory
{
    //Se podria argumentar que es mejor tener una funcion Attack() y que se implemente distinto
    //Lo que quiero mostrar es que cada tipo de Enemy tiene funciones unicas
    public abstract class Enemy : MonoBehaviour {}

    public abstract class MeleeEnemy : Enemy
    {
        protected virtual void Start() { MeleeAttack(); }
        protected abstract void MeleeAttack();
    }

    public abstract class RangedEnemy : Enemy
    {
        protected virtual void Start() { RangedAttack(); }
        protected abstract void RangedAttack();
    }

    public abstract class FlyingEnemy : Enemy
    {
        protected virtual void Start() { FlyingAttack(); }
        protected abstract void FlyingAttack();
    }
}
