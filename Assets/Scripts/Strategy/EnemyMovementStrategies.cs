using UnityEngine;

namespace ShadowExit.EnemyStrategySystem
{
    public class FollowMovementStrategy : EnemyMovementStrategy
    {
        private readonly float minDistance;

        public FollowMovementStrategy(Transform transform, Rigidbody2D rb, float speed, Transform target, float minDistance)
            : base(transform, rb, speed, target)
        {
            this.minDistance = minDistance;
        }

        public override void Move(float deltaTime)
        {
            if (target == null)
            {
                return;
            }

            if (Vector2.Distance(transform.position, target.position) <= minDistance)
            {
                return;
            }

            Vector2 nextPosition = Vector2.MoveTowards(rb.position, target.position, speed * deltaTime);
            rb.MovePosition(nextPosition);
        }
    }

    public class PatrolMovementStrategy : EnemyMovementStrategy
    {
        private readonly Transform[] waypoints;
        private readonly float chaseRange;
        private readonly float stopDistance;
        private readonly float waitTime;

        private int currentWaypoint;
        private float waitTimer;

        public PatrolMovementStrategy(
            Transform transform,
            Rigidbody2D rb,
            float speed,
            Transform target,
            Transform[] waypoints,
            float chaseRange,
            float stopDistance,
            float waitTime) : base(transform, rb, speed, target)
        {
            this.waypoints = waypoints;
            this.chaseRange = chaseRange;
            this.stopDistance = stopDistance;
            this.waitTime = waitTime;
        }

        public override void Move(float deltaTime)
        {
            if (ShouldChaseTarget())
            {
                ChaseTarget(deltaTime);
                return;
            }

            Patrol(deltaTime);
        }

        private bool ShouldChaseTarget()
        {
            if (target == null)
            {
                return false;
            }

            return Vector2.Distance(transform.position, target.position) <= chaseRange;
        }

        private void ChaseTarget(float deltaTime)
        {
            if (Vector2.Distance(transform.position, target.position) <= stopDistance)
            {
                return;
            }

            Vector2 nextPosition = Vector2.MoveTowards(rb.position, target.position, speed * deltaTime);
            rb.MovePosition(nextPosition);
        }

        private void Patrol(float deltaTime)
        {
            if (waypoints == null || waypoints.Length == 0)
            {
                return;
            }

            Transform currentTarget = waypoints[currentWaypoint];
            if (currentTarget == null)
            {
                AdvanceWaypoint();
                return;
            }

            if (Vector2.Distance(transform.position, currentTarget.position) > 0.05f)
            {
                Vector2 nextPosition = Vector2.MoveTowards(rb.position, currentTarget.position, speed * deltaTime);
                rb.MovePosition(nextPosition);
                waitTimer = 0f;
                return;
            }

            waitTimer += deltaTime;
            if (waitTimer >= waitTime)
            {
                waitTimer = 0f;
                AdvanceWaypoint();
            }
        }

        private void AdvanceWaypoint()
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}
