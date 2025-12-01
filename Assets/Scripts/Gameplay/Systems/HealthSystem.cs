using System;
using TowerDefence.Gameplay.Stats;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public class HealthSystem : IHealthSystem
    {
        public event IHealthSystem.DamageTakenHandler damageTaken;
        public event Action died;

        private readonly Stat _healthStat;

        public HealthSystem(Stat healthStat)
        {
            _healthStat = healthStat;
        }

        public void TakeDamage(float amount, IEntity attacker)
        {
            if (_healthStat.value <= 0)
                return;

            _healthStat.value -= amount;
            damageTaken?.Invoke(attacker);

            if (_healthStat.value <= 0)
                died?.Invoke();
        }

        public void RestoreAll()
        {
            _healthStat.value = _healthStat.maxValue;
        }
    }
}

