using TowerDefence.Core;
using TowerDefence.Gameplay;

namespace TowerDefence.Systems
{
    public interface IEntitySpawner : IService
    {
        delegate void EntitySpawnedHandler(Entity entity);


        event EntitySpawnedHandler playerSpawned;
        event EntitySpawnedHandler entitySpawned;
        Entity SpawnPlayer(Team team);
        Entity SpawnEntity(Team team);
        void Clear();
    }
}

