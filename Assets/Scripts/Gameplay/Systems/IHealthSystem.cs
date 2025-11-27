using System;

namespace TowerDefence.Gameplay.Systems
{
    public interface IHealthSystem
    {
        event Action onDeath;
        void TakeDamage(float amount);
        void RestoreAll();
    }
}

