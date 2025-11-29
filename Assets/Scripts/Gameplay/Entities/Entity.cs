using System;
using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.States;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace TowerDefence.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Entity : MonoBehaviour, IEntity
    {
        public event Action<IEntity> onDeath;

        public Team team => _team;
        public Race race => _race;
        public bool isAlive => _health.value > 0;

        private Team _team;
        private Race _race;
        private Dictionary<StatType, Stat> _stats;
        private Stat _health;
        private IHealthSystem _healthSystem;
        private IAttackSystem _attackSystem;
        private IStateMachine _stateMachine;
        private Dictionary<Type, IState> _states;
        private ICommandCenter _commandCenter;
        private Rigidbody _rigidbody;

        public void Init(Team team,
                         Race race,
                         Dictionary<StatType, Stat> stats, 
                         ICommandCenter commandCenter)
        {
            _team = team;
            _race = race;
            _stats = stats;
            _health = _stats.GetValueOrDefault(StatType.Health);
            _commandCenter = commandCenter;

            var factory = Services.Get<FactoryService>().gameplay;
            _healthSystem = factory.CreateHealthSystem(_health);
            _attackSystem = factory.CreateAttackSystem(this, StatType.AttackSpeed);

            //Create and populate state machine
            _stateMachine = factory.CreateEntityStateMachine();
            _states = new Dictionary<Type, IState>() 
            {
                { typeof(IdleState), new IdleState(this, _commandCenter, _attackSystem) },
                { typeof(MoveState), new MoveState(this, _rigidbody, _commandCenter, _attackSystem) },
                { typeof(AttackState), new AttackState(this, _rigidbody, _commandCenter, _attackSystem) }
            };

            SetState<IdleState>();
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
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

        public void ApplyDamage(float value, IEntity attacker)
        {
            _healthSystem.TakeDamage(value);
        }

        private void Update()
        {
            _commandCenter.Tick(Time.deltaTime);
            _stateMachine?.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _commandCenter.Dispose();
        }
    }
}
