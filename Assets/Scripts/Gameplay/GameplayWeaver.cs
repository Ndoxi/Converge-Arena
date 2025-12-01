using System;
using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Game;
using TowerDefence.Gameplay.Cameras;
using TowerDefence.Gameplay.Systems;
using TowerDefence.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class GameplayWeaver : IDisposable
    {
        private IGameplayCameraService _cameraService;
        private IEntitySpawner _spawner;
        private IMatchStateService _matchState;
        private IEventBus _eventBus;
        private ITeamConversionSystem _teamConversionSystem;
        private ITargetingService _targeting;
        private Entity _player;
        private readonly List<Entity> _entities = new List<Entity>(64);

        public void Init()
        {
            _cameraService = Services.Get<IGameplayCameraService>();
            _spawner = Services.Get<IEntitySpawner>();
            _matchState = Services.Get<IMatchStateService>();
            _eventBus = Services.Get<IEventBus>();
            _teamConversionSystem = Services.Get<ITeamConversionSystem>();
            _targeting = Services.Get<ITargetingService>();

            _spawner.playerSpawned += OnPlayerSpawned;
            _spawner.entitySpawned += OnEntitySpawned;
            _teamConversionSystem.entityConverted += OnEntityConverted;
        }

        public void Dispose()
        {
            _spawner.playerSpawned -= OnPlayerSpawned;
            _spawner.entitySpawned -= OnEntitySpawned;
            _teamConversionSystem.entityConverted -= OnEntityConverted;

            if (_player != null)
                _player.died -= OnPlayerDeath;

            _entities.Clear();
        }

        private void OnPlayerSpawned(Entity player)
        {
            if (_player != null)
                _player.died -= OnPlayerDeath;
            _player = player;
            _player.died += OnPlayerDeath;

            _cameraService.gameplayCamera.Follow(player.transform);

            CheckGameOver();
        }

        private void OnEntitySpawned(Entity entity)
        {
            if (_entities.Contains(entity))
                return;
            _entities.Add(entity);
        }

        private void OnPlayerDeath(IEntity entity, IEntity killer)
        {
            _matchState.SetResult(false);
            _eventBus.Publish(new GameOverEvent());
        }

        private void OnEntityConverted(IEntity entity, Team oldTeam, Team newTeam)
        {
            CheckGameOver();
        }

        private void CheckGameOver()
        {
            if (_player == null)
            {
                Debug.LogWarning("Indefined behaviour expected. Player is null.");
                return;
            }

            if (AllEnemiesAreDead())
            {
                _matchState.SetResult(true);
                _eventBus.Publish(new GameOverEvent());
            }
        }

        private bool AllEnemiesAreDead()
        {
            foreach (var entity in _entities)
            {
                if (_targeting.IsEnemy(_player, entity))
                    return false;
            }
            return true;
        }
    }
}