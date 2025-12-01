using System;
using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.States;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Entity : MonoBehaviour, IEntity
    {
        public delegate void TeamChangedDelegate(Team team);

        public event IEntity.EntityDiedHandler died;
        public event TeamChangedDelegate teamChanged;

        public Team team 
        { 
            get => _team;
            set 
            {
                if (_team == value)
                    return;

                _team = value;
                teamChanged?.Invoke(_team);
            }
        }

        public Race race => _race;
        public IHealthSystem healthSystem => _healthSystem;
        public bool isAlive => _stateMachine?.CurrentState != _deathState;

        private Team _team;
        private Race _race;
        private Dictionary<StatType, Stat> _stats;
        private IHealthSystem _healthSystem;
        private IAttackSystem _attackSystem;
        private IStateMachine _stateMachine;
        private Dictionary<Type, IState> _states;
        private DeathState _deathState;
        private ICommandCenter _commandCenter;
        private Rigidbody _rigidbody;
        private IEntity _lastAttacker;

        public void Init(Team team,
                         Race race,
                         Dictionary<StatType, Stat> stats, 
                         ICommandCenter commandCenter)
        {
            _team = team;
            _race = race;
            _stats = stats;
            _commandCenter = commandCenter;

            var factory = Services.Get<FactoryService>().gameplay;
            _healthSystem = factory.CreateHealthSystem(this, StatType.Health);
            _attackSystem = factory.CreateAttackSystem(this, StatType.AttackSpeed);

            //Create and populate state machine
            _stateMachine = factory.CreateEntityStateMachine();
            _deathState = new DeathState();
            _states = new Dictionary<Type, IState>() 
            {
                { typeof(IdleState), new IdleState(this, _rigidbody, _commandCenter, _attackSystem) },
                { typeof(MoveState), new MoveState(this, _rigidbody, _commandCenter, _attackSystem) },
                { typeof(AttackState), new AttackState(this, _rigidbody, _commandCenter, _attackSystem) },
                { typeof(DeathState), _deathState }
            };

            _healthSystem.damageTaken += OnDamage;
            _healthSystem.died += OnDeath;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetIdle()
        {
            SetState<IdleState>();
        }

        public void SetState<TState>(IStateContext context = null) where TState : IState
        {
            var state = _states.GetValueOrDefault(typeof(TState));
            _stateMachine.SetState(state, context);
        }

        public Stat GetStat(StatType statType)
        {
            return _stats.GetValueOrDefault(statType);
        }

        private void Update()
        {
            _commandCenter?.Tick(Time.deltaTime);
            _stateMachine?.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _commandCenter.Dispose();
            _attackSystem.Dispose();

            _healthSystem.damageTaken -= OnDamage;
            _healthSystem.died -= OnDeath;
        }

        private void OnDamage(IEntity attacker)
        {
            _lastAttacker = attacker;
        }

        private void OnDeath()
        {
            SetState<DeathState>();
            died?.Invoke(this, _lastAttacker);
        }
    }
}
