using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public class WorldPointsProvider : IWorldPointsService
    {
        private readonly WorldPoint _playerSpawnPoint;

        public WorldPointsProvider(WorldPoint playerSpawnPoint)
        {
            _playerSpawnPoint = playerSpawnPoint;
        }

        public void Init() { }

        public WorldPoint GetPlayerSpawnPoint()
        {
            return _playerSpawnPoint;
        }
    }
}