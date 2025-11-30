using System;
using TowerDefence.Gameplay;

namespace TowerDefence.Data
{
    [Serializable]
    public struct TeamData
    {
        public Team team;
        public int spawnCount;
    }
}