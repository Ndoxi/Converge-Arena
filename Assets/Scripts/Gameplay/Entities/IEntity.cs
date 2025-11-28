using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.Stats;

namespace TowerDefence.Gameplay
{
    public interface IEntity
    {
        event Action<IEntity> onDeath;
        Team team { get; }
        Race race { get; }
        bool isAlive { get; }
        Stat GetStat(StatType statType);
        void ApplyDamage(float value, IEntity attacker);
        void SetState<T>(IStateContext context = null) where T : IState;
    }
}
