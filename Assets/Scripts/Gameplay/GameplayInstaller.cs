using TowerDefence.Core;
using TowerDefence.Gameplay.Cameras;
using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class GameplayInstaller : MonoBehaviour
    {
        [SerializeField] private GameplayCamera _gameplayCamera;
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

            _gameplayWeaver = new GameplayWeaver();
            _gameplayWeaver.Init();
        }

        private void ClearContext()
        {
            _serviceLocator.Unregister<IGameplayCameraService>();
            _gameplayWeaver.Dispose();
        }
    }
}