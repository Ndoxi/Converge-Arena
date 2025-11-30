using TowerDefence.Core;

namespace TowerDefence.Gameplay.Systems
{
    public interface ISpawnPointsService : IService
    {
        SpawnPoint GetPlayerSpawnPoint();
    }
}