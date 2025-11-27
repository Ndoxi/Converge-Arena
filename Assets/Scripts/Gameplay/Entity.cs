using System;
using System.Collections.Generic;
using System.Diagnostics;
using TowerDefence.Core;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay
{
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

        public void Init(Team team,
                         Race race,
                         Dictionary<StatType, Stat> stats)
        {
            _team = team;
            _race = race;
            _stats = stats;
            _health = _stats.GetValueOrDefault(StatType.Health);

            var factory = Services.Get<FactoryService>().gameplay;
            _healthSystem = factory.CreateHealthSystem(_health);
            _attackSystem = factory.CreateAttackSystem(this, StatType.AttackSpeed);
        }

        public Stat GetStat(StatType statType)
        {
            return _stats.GetValueOrDefault(statType);
        }

        public void ApplyDamage(float value, IEntity attacker)
        {
            _healthSystem.TakeDamage(value);
        }
    }
}
