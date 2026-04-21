using UnityEngine;

namespace ShadowExit.PlayerStateSystem
{
    public enum PlayerStateKey
    {
        Idle,
        Move,
        Attack,
        Hurt,
        Dead
    }

    public class PlayerIdleState : State<PlayerBrain>
    {
        public PlayerIdleState(PlayerBrain target) : base(target) { }

        public override void OnEnter() { }

        public override void OnUpdate()
        {
            if (target.Health != null && target.Health.IsDead)
            {
                target.ChangeState(PlayerStateKey.Dead);
                return;
            }

            if (target.Attack != null && target.Attack.CanAttack() && target.Attack.WasAttackPressedThisFrame())
            {
                target.ChangeState(PlayerStateKey.Attack);
                return;
            }

            if (target.HasMoveInput)
            {
                target.ChangeState(PlayerStateKey.Move);
            }
        }

        public override void OnFixedUpdate() { }

        public override void OnExit() { }
    }

    public class PlayerMoveState : State<PlayerBrain>
    {
        public PlayerMoveState(PlayerBrain target) : base(target) { }

        public override void OnEnter() { }

        public override void OnUpdate()
        {
            if (target.Health != null && target.Health.IsDead)
            {
                target.ChangeState(PlayerStateKey.Dead);
                return;
            }

            if (target.Attack != null && target.Attack.CanAttack() && target.Attack.WasAttackPressedThisFrame())
            {
                target.ChangeState(PlayerStateKey.Attack);
                return;
            }

            if (!target.HasMoveInput)
            {
                target.ChangeState(PlayerStateKey.Idle);
            }
        }

        public override void OnFixedUpdate()
        {
            target.Movement?.TickMovement(Time.fixedDeltaTime);
        }

        public override void OnExit() { }
    }

    public class PlayerAttackState : State<PlayerBrain>
    {
        private float timer;

        public PlayerAttackState(PlayerBrain target) : base(target) { }

        public override void OnEnter()
        {
            timer = target.AttackStateDuration;
            target.Attack?.PerformAttack();
        }

        public override void OnUpdate()
        {
            if (target.Health != null && target.Health.IsDead)
            {
                target.ChangeState(PlayerStateKey.Dead);
                return;
            }

            timer -= Time.deltaTime;
            if (timer > 0f)
            {
                return;
            }

            target.ChangeState(target.HasMoveInput ? PlayerStateKey.Move : PlayerStateKey.Idle);
        }

        public override void OnFixedUpdate() { }

        public override void OnExit() { }
    }

    public class PlayerHurtState : State<PlayerBrain>
    {
        private float timer;

        public PlayerHurtState(PlayerBrain target) : base(target) { }

        public override void OnEnter()
        {
            timer = target.HurtStateDuration;
        }

        public override void OnUpdate()
        {
            if (target.Health != null && target.Health.IsDead)
            {
                target.ChangeState(PlayerStateKey.Dead);
                return;
            }

            timer -= Time.deltaTime;
            if (timer > 0f)
            {
                return;
            }

            target.ChangeState(target.HasMoveInput ? PlayerStateKey.Move : PlayerStateKey.Idle);
        }

        public override void OnFixedUpdate() { }

        public override void OnExit() { }
    }

    public class PlayerDeadState : State<PlayerBrain>
    {
        public PlayerDeadState(PlayerBrain target) : base(target) { }

        public override void OnEnter() { }

        public override void OnUpdate() { }

        public override void OnFixedUpdate() { }

        public override void OnExit() { }
    }
}
