using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Data;
using TowerDefence.Data.Constants;
using TowerDefence.Gameplay;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.States;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;

namespace TowerDefence.Systems
{
    public class EntityConfigurator : IEntityConfigurator
    {
        private EntitiesConfig _config;
        private ITeamConversionSystem _teamConversionSystem;

        public void Init()
        {
            _config = Services.Get<IConfigProvider>().Get<EntitiesConfig>(ConfigNames.EntitiesConfig);
            _teamConversionSystem = Services.Get<ITeamConversionSystem>();
        }

        public void Configure(Entity entity, Team team)
        {
            entity.Init(team, Race.None, GetStats(), new AICommandCenter());
            entity.SetIdle();
            _teamConversionSystem.Register(entity);
        }

        public void ConfigurePlayer(Entity entity, Team team)
        {
            entity.Init(team, Race.None, GetStats(), new PlayerCommandCenter());
            entity.SetIdle();
        }

        public void Clear()
        {
            _teamConversionSystem.Clear();
        }

        private Dictionary<StatType, Stat> GetStats()
        {
            var stats = new Dictionary<StatType, Stat>();
            foreach (var statData in _config.entityData.stats)
            {
                stats.Add(statData.statType, new Stat(statData.value, statData.minValue, statData.minValue));
            }

            return stats;
        }
    }
}

