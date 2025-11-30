using TowerDefence.Core;

namespace TowerDefence.Gameplay.Systems
{
    public interface IWorldPointsService : IService
    {
        IWorldPoint GetSpawnPoint(Team team);
        IWorldPoint[] GetAIWaypoints();
    }
}