using System;
using TowerDefence.Core;
using TowerDefence.Gameplay;

namespace TowerDefence.Systems
{
    public interface ILevelBuilder : IService
    {
        delegate void PlayerSpawnedHandler(Entity playerEntity);

        event PlayerSpawnedHandler playerSpawned;

        void Load();
        void Unload();
    }
}

