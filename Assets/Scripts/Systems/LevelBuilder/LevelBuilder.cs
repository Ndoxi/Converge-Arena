using TowerDefence.Core;
using TowerDefence.Gameplay;
using UnityEngine;

namespace TowerDefence.Systems
{
    public class LevelBuilder : ILevelBuilder
    {
        private GameplayServicesFactory _factory;
        private Entity _player;

        public void Init()
        {
            _factory = Services.Get<FactoryService>().gameplay;
        }

        public void Load()
        {
            _player = _factory.CreatePlayer(Team.None, Race.None);
        }

        public void Unload()
        {
            Object.Destroy(_player);
        }
    }
}

