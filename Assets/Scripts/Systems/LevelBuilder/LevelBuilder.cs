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
            _entityConfigurator = Services.Get<EntityConfigurator>();
        }

        public void Load()
        {
            _player = _factory.CreateGenericEntity(Vector3.zero, Quaternion.identity);
            _entityConfigurator.ConfigurePlayer(_player);
        }

        public void Unload()
        {
            Object.Destroy(_player);

            foreach (var entity in _managed)
            {
                Object.Destroy(entity);
            }

            _managed.Clear();
        }
    }
}

