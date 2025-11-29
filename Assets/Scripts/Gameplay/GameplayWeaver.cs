using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.Cameras;
using TowerDefence.Systems;

namespace TowerDefence.Gameplay
{
    public class GameplayWeaver : IDisposable
    {
        private IGameplayCameraService _cameraService;
        private ILevelBuilder _levelBuilder;

        public void Init()
        {
            _cameraService = Services.Get<IGameplayCameraService>();
            _levelBuilder = Services.Get<ILevelBuilder>();
            _levelBuilder.playerSpawned += OnPlayerSpawned;
        }

        public void Dispose()
        {
            _levelBuilder.playerSpawned -= OnPlayerSpawned;
        }

        private void OnPlayerSpawned(Entity player)
        {
            _cameraService.gameplayCamera.Follow(player.transform);
        }
    }
}