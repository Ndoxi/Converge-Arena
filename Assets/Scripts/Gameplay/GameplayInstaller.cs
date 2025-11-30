using TowerDefence.Core;
using TowerDefence.Gameplay.Cameras;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class GameplayInstaller : MonoBehaviour
    {
        [SerializeField] private GameplayCamera _gameplayCamera;
        [SerializeField] private SpawnPoint _playerSpawnPoint;

        private IServiceLocator _serviceLocator;
        private GameplayWeaver _gameplayWeaver;

        private void Awake()
        {
            _serviceLocator = Services.Get<IServiceLocator>();
            CreateContext();
        }

        private void OnDestroy()
        {
            ClearContext();
        }

        private void CreateContext()
        {
            var cameraService = new GameplayCameraService(_gameplayCamera);
            cameraService.Init();
            _serviceLocator.Register<IGameplayCameraService>(cameraService);

            var spawnPointsProvider = new SpawnPointsProvider(_playerSpawnPoint);
            spawnPointsProvider.Init();
            _serviceLocator.Register<ISpawnPointsService>(spawnPointsProvider);

            _gameplayWeaver = new GameplayWeaver();
            _gameplayWeaver.Init();
        }

        private void ClearContext()
        {
            _serviceLocator.Unregister<IGameplayCameraService>();
            _serviceLocator.Unregister<ISpawnPointsService>();

            _gameplayWeaver.Dispose();
        }
    }
}