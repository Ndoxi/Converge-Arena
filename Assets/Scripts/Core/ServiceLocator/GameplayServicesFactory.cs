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

namespace TowerDefence.Core
{
    public class GameplayServicesFactory : IService
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

        public Entity CreatePlayer(Team team, Race race)
        {
            var entity = CreateGenericEntity(team, race, new PlayerCommandCenter());
            return entity;
        }

        public Entity CreateEntity(Team team, Race race)
        {
            var entity = CreateGenericEntity(team, race, null);
            return entity;
        }

        private Entity CreateGenericEntity(Team team, Race race, ICommandCenter commandCenter)
        {
            var entity = UnityEngine.Object.Instantiate(_config.entityPrefab);
            var stats = new Dictionary<StatType, Stat>();
            foreach (var statData in _config.entityData.stats)
            {
                stats.Add(statData.statType, new Stat(statData.value, statData.minValue, statData.minValue));
            }
            entity.Init(team, race, stats, commandCenter);

            return entity;
        }
    }
}
