using System;
using System.Collections.Generic;
using System.Diagnostics;
using TowerDefence.Data;
using TowerDefence.Data.Constants;
using TowerDefence.Gameplay;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.States;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Core
{
    public class GameplayFactory : IService
    {
        private readonly List<IDisposable> _managed = new(100);
        private EntitiesConfig _config;

        public void Init() 
        {
            _config = Services.Get<IConfigProvider>().Get<EntitiesConfig>(ConfigNames.EntitiesConfig);
        }

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

        public IStateMachine CreateEntityStateMachine()
        {
            var stateMachine = new StateMachine();
            stateMachine.Init();
            return stateMachine;
        }

        public Entity CreateGenericEntity(Vector3 position, Quaternion rotation)
        {
            return UnityEngine.Object.Instantiate(_config.entityPrefab, position, rotation);
        }
    }
}
