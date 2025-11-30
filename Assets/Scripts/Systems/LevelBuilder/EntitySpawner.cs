using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Gameplay;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Systems
{
    public class EntitySpawner : IEntitySpawner
    {
        private readonly List<Entity> _managed = new List<Entity>(64);
        private GameplayFactory _factory;
        private IEntityConfigurator _entityConfigurator;
        private IWorldPointsService _worldPointsService;


        public void Init()
        {
            _factory = Services.Get<FactoryService>().gameplay;
            _entityConfigurator = Services.Get<IEntityConfigurator>();
            _worldPointsService = Services.Get<IWorldPointsService>();
        }

        public void Clear()
        {
            foreach (var entity in _managed)
            {
                if (entity != null)
                    Object.Destroy(entity);
            }

            _managed.Clear();
        }

        public Entity SpawnPlayer(Team team)
        {
            var point = _worldPointsService.GetSpawnPoint(team);
            var player = _factory.CreateGenericEntity(RadomizePosition(point.position), point.rotation);
            _entityConfigurator.ConfigurePlayer(player, team);
            _managed.Add(player);

            return player;
        }

        public Entity SpawnEntity(Team team)
        {
            var point = _worldPointsService.GetSpawnPoint(team);
            var entity = _factory.CreateGenericEntity(RadomizePosition(point.position), point.rotation);
            _entityConfigurator.Configure(entity, team);
            _managed.Add(entity);

            return entity;
        }

        private Vector3 RadomizePosition(Vector3 center, float radius = 1f)
        {
            float offsetX = Random.Range(-radius, radius);
            float offsetZ = Random.Range(-radius, radius);
            return center + new Vector3(offsetX, 0f, offsetZ);
        }
    }
}

