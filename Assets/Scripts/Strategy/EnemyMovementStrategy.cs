using UnityEngine;

namespace ShadowExit.EnemyStrategySystem
{
    public abstract class EnemyMovementStrategy
    {
        protected readonly Transform transform;
        protected readonly Rigidbody2D rb;
        protected readonly float speed;
        protected readonly Transform target;

        protected EnemyMovementStrategy(Transform transform, Rigidbody2D rb, float speed, Transform target)
        {
            this.transform = transform;
            this.rb = rb;
            this.speed = speed;
            this.target = target;
        }

        public abstract void Move(float deltaTime);
    }
}
