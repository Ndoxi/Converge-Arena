using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Gameplay;
using UnityEngine;

namespace TowerDefence.Systems
{
    public class LevelBuilder : ILevelBuilder
    {
        private GameplayFactory _factory;
        private IEntityConfigurator _entityConfigurator;
        private Entity _player;
        private readonly List<Entity> _managed = new List<Entity>(64);

        public void Init()
        {
            _factory = Services.Get<FactoryService>().gameplay;
            _entityConfigurator = Services.Get<IEntityConfigurator>();
        }

        public void Load()
        {
            _player = CreatePlayer();
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

        private Entity CreatePlayer()
        {
            var player = _factory.CreateGenericEntity(Vector3.zero, Quaternion.identity);
            _entityConfigurator.ConfigurePlayer(player);

            return player;
        }
    }
}

