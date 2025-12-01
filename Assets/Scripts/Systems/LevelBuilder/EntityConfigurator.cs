using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Data;
using TowerDefence.Data.Constants;
using TowerDefence.Gameplay;
using TowerDefence.Gameplay.Commands;
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
            var brain = new AIBrainCommandCenter(entity);

            var decorator = new EntityDecorator();
            decorator.Init(entity);

            entity.Init(team, Race.None, GetStats(), brain, decorator);
            entity.SetIdle();
            decorator.Decorate();
            brain.Activate();

            _teamConversionSystem.Register(entity);
        }

        public void ConfigurePlayer(Entity entity, Team team)
        {
            var decorator = new PlayerEntityDecorator();
            decorator.Init(entity);

            entity.Init(team, Race.None, GetStats(), new PlayerCommandCenter(), decorator);
            entity.SetIdle();
            decorator.Decorate();
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
                stats.Add(statData.statType, new Stat(statData.value, statData.minValue, statData.maxValue));
            }

            return stats;
        }
    }
}

