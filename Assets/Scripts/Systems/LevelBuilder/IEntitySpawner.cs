using TowerDefence.Core;
using TowerDefence.Gameplay;

namespace TowerDefence.Systems
{
    public interface IEntitySpawner : IService
    {
        Entity SpawnPlayer(Team team);
        Entity SpawnEntity(Team team);
        void Clear();
    }
}

