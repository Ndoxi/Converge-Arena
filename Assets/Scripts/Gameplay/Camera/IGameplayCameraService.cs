using TowerDefence.Core;

namespace TowerDefence.Gameplay.Cameras
{
    public interface IGameplayCameraService : IService
    {
        GameplayCamera gameplayCamera { get; }
    }
}