using TowerDefence.Core;

namespace TowerDefence.Gameplay.Systems
{
    public interface IWorldPointsService : IService
    {
        WorldPoint GetPlayerSpawnPoint();
    }
}