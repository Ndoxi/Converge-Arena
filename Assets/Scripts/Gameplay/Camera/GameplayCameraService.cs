namespace TowerDefence.Gameplay.Cameras
{
    public class GameplayCameraService : IGameplayCameraService
    {
        public GameplayCamera gameplayCamera => _gameplayCamera;

        private readonly GameplayCamera _gameplayCamera;

        public GameplayCameraService(GameplayCamera gameplayCamera)
        {
            _gameplayCamera = gameplayCamera;
        }

        public void Init() 
        {
            _gameplayCamera.Init();
        }
    }
}