using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public class SpawnPointsProvider : ISpawnPointsService
    {
        private readonly SpawnPoint _playerSpawnPoint;

        public SpawnPointsProvider(SpawnPoint playerSpawnPoint)
        {
            _playerSpawnPoint = playerSpawnPoint;
        }

        public void Init() { }

        public SpawnPoint GetPlayerSpawnPoint()
        {
            return _playerSpawnPoint;
        }
    }
}