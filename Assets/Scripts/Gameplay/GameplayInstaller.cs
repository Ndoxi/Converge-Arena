using TowerDefence.Core;
using TowerDefence.Data;
using TowerDefence.Gameplay.Cameras;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class GameplayInstaller : MonoBehaviour
    {
        [SerializeField] private GameplayCamera _gameplayCamera;
        [Header("World Points")]
        [SerializeField] private SpawnWorldPoint[] _spawnPoints;
        [SerializeField] private WorldPoint[] _aiWaypoints;

        private IServiceLocator _serviceLocator;
        private GameplayWeaver _gameplayWeaver;
        private EntityGroupSystem _groupSystem;

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

            var worldPointsProvider = new WorldPointsProvider(_spawnPoints, _aiWaypoints);
            worldPointsProvider.Init();
            _serviceLocator.Register<IWorldPointsService>(worldPointsProvider);

            _groupSystem = new EntityGroupSystem();
            _groupSystem.Init();
            _serviceLocator.Register<IEntityGroupSystem>(_groupSystem);

            _gameplayWeaver = new GameplayWeaver();
            _gameplayWeaver.Init();
        }

        private void ClearContext()
        {
            _serviceLocator.Unregister<IGameplayCameraService>();
            _serviceLocator.Unregister<IWorldPointsService>();

            _groupSystem.Dispose();
            _gameplayWeaver.Dispose();
        }
    }
}