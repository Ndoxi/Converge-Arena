using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;

namespace TowerDefence.Gameplay
{
    public interface IEntity
    {
        delegate void EntityDiedHandler(IEntity entity, IEntity killer);

        event EntityDiedHandler died;
        Team team { get; set; }
        Race race { get; }
        IHealthSystem healthSystem { get; }
        bool isAlive { get; }
        Stat GetStat(StatType statType);
        void SetIdle();
        void SetState<T>(IStateContext context = null) where T : IState;
    }
}
