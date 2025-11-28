using System;
using System.Collections.Generic;
using TowerDefence.Gameplay;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;

namespace TowerDefence.Core
{
    public class GameplayServicesFactory : IService
    {
        private readonly List<IDisposable> _managed = new(100);

        public void Init() { }

        public void Clear()
        {
            foreach (IDisposable disposable in _managed) 
            {
                disposable.Dispose();
            }

            _managed.Clear();
        }

        public IHealthSystem CreateHealthSystem(Stat healthStat)
        {
            return new HealthSystem(healthStat);
        }

        public IAttackSystem CreateAttackSystem(IEntity owner,
                                                StatType attackSpeedStatType)
        {
            var system = new AttackSystem(owner, attackSpeedStatType);
            _managed.Add(system);
            return system;
        }

        public IStateMachine CreateStateMachine()
        {
            var stateMachine = new StateMachine();
            stateMachine.Init();
            return stateMachine;
        }
    }
}
