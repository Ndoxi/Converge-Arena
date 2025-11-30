using System.Drawing;
using TowerDefence.Core;
using TowerDefence.Data;
using TowerDefence.Data.Constants;

namespace TowerDefence.Systems
{
    public class LevelBuilder : ILevelBuilder
    {
        public event ILevelBuilder.PlayerSpawnedHandler playerSpawned;

        private LevelConfig _config;
        private IEntitySpawner _spawner;

        public void Init()
        {
            _config = Services.Get<IConfigProvider>().Get<LevelConfig>(ConfigNames.LevelConfig);
            _spawner = Services.Get<IEntitySpawner>();
        }

        public void Load()
        {
            foreach (var teamData in _config.teamDatas)
            {
                for (int i = 0; i < teamData.spawnCount; i++)
                {
                    _spawner.SpawnEntity(teamData.team);
                }
            }

            var player = _spawner.SpawnPlayer(_config.playersTeam);
            playerSpawned?.Invoke(player);
        }

        public void Unload()
        {
            _spawner.Clear();
        }
    }
}

