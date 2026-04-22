using System.Collections.Generic;
using UnityEngine;

namespace ShadowExit.PlayerStateSystem
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(PlayerAttack))]
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerBrain : MonoBehaviour
    {
        [SerializeField] private float attackStateDuration = 0.15f;
        [SerializeField] private float hurtStateDuration = 0.2f;

        public Movement Movement { get; private set; }
        public PlayerAttack Attack { get; private set; }
        public PlayerHealth Health { get; private set; }
        public float AttackStateDuration => attackStateDuration;
        public float HurtStateDuration => hurtStateDuration;
        public bool HasMoveInput => Movement != null && Movement.HasMoveInput;

        // El brain es el contexto del patron State: guarda y cambia el estado actual del player.
        private readonly Dictionary<PlayerStateKey, State<PlayerBrain>> allStates = new();
        private State<PlayerBrain> currentState;

        private void Awake()
        {
            Movement = GetComponent<Movement>();
            Attack = GetComponent<PlayerAttack>();
            Health = GetComponent<PlayerHealth>();

            InitializeStates();
        }

        private void OnEnable()
        {
            if (Health != null)
            {
                Health.Damaged += HandleDamaged;
                Health.Died += HandleDied;
            }
        }

        private void Start()
        {
            ChangeState(PlayerStateKey.Idle);
        }

        private void OnDisable()
        {
            if (Health != null)
            {
                Health.Damaged -= HandleDamaged;
                Health.Died -= HandleDied;
            }
        }

        private void Update()
        {
            currentState?.OnUpdate();
        }

        private void FixedUpdate()
        {
            currentState?.OnFixedUpdate();
        }

        public void ChangeState(PlayerStateKey nextState)
        {
            if (!allStates.TryGetValue(nextState, out State<PlayerBrain> newState))
            {
                return;
            }

            if (currentState == newState)
            {
                return;
            }

            // Todas las transiciones pasan por aca para asegurar salida, entrada y log consistente.
            string previousStateName = currentState != null ? currentState.GetType().Name : "None";
            currentState?.OnExit();
            currentState = newState;
            Debug.Log($"[PlayerBrain] cambio estado {previousStateName} a {currentState.GetType().Name}", this);
            currentState.OnEnter();
        }

        private void InitializeStates()
        {
            // Se registra una sola instancia por estado para reutilizarla durante toda la partida.
            allStates.Clear();
            allStates.Add(PlayerStateKey.Idle, new PlayerIdleState(this));
            allStates.Add(PlayerStateKey.Move, new PlayerMoveState(this));
            allStates.Add(PlayerStateKey.Attack, new PlayerAttackState(this));
            allStates.Add(PlayerStateKey.Hurt, new PlayerHurtState(this));
            allStates.Add(PlayerStateKey.Dead, new PlayerDeadState(this));
        }

        private void HandleDamaged()
        {
            if (Health != null && !Health.IsDead)
            {
                ChangeState(PlayerStateKey.Hurt);
            }
        }

        private void HandleDied()
        {
            ChangeState(PlayerStateKey.Dead);
        }
    }
}
