using System;
using TowerDefence.Gameplay.Stats;

namespace TowerDefence.Gameplay.Systems
{
    public class HealthSystem : IHealthSystem
    {
        public event Action onDeath;

        private readonly Stat _healthStat;

        public HealthSystem(Stat healthStat)
        {
            _healthStat = healthStat;
        }

        public void TakeDamage(float amount)
        {
            if (_healthStat.value <= 0)
                return;

            _healthStat.value -= amount;

            if (_healthStat.value <= 0)
                onDeath?.Invoke();
        }

        public void RestoreAll()
        {
            _healthStat.value = _healthStat.maxValue;
        }
    }
}

