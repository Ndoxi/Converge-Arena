using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Gameplay;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Systems
{
    public class LevelBuilder : ILevelBuilder
    {
        public event ILevelBuilder.PlayerSpawnedHandler playerSpawned;

        private GameplayFactory _factory;
        private IEntityConfigurator _entityConfigurator;
        private ISpawnPointsService _spawnPointsService;
        private Entity _player;
        private readonly List<Entity> _managed = new List<Entity>(64);


        public void Init()
        {
            _factory = Services.Get<FactoryService>().gameplay;
            _entityConfigurator = Services.Get<IEntityConfigurator>();
            _spawnPointsService = Services.Get<ISpawnPointsService>();
        }

        public void Load()
        {
            var spawnPoint = _spawnPointsService.GetPlayerSpawnPoint();
            _player = CreatePlayer(spawnPoint.position, spawnPoint.rotation);
            playerSpawned?.Invoke(_player);
        }

        public void Unload()
        {
            if (_player != null)
                Object.Destroy(_player);

            foreach (var entity in _managed)
            {
                if (entity != null)
                    Object.Destroy(entity);
            }

            _managed.Clear();
        }

        private Entity CreatePlayer(Vector3 position, Quaternion rotation)
        {
            var player = _factory.CreateGenericEntity(position, rotation);
            _entityConfigurator.ConfigurePlayer(player);

            return player;
        }
    }
}

