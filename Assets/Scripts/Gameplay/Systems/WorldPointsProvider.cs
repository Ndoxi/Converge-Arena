using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TowerDefence.Data;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public class WorldPointsProvider : IWorldPointsService
    {
        private readonly Dictionary<Team, SpawnWorldPoint[]> _teamSpawnPoints;
        private readonly IWorldPoint[] _aiWaypoints;

        public WorldPointsProvider(SpawnWorldPoint[] spawnWorldPoints, IWorldPoint[] aiWaypoints)
        {
            _teamSpawnPoints = spawnWorldPoints
                .GroupBy(s => s.team)
                .ToDictionary(g => g.Key, g => g.Select(s => s).ToArray());

            _aiWaypoints = aiWaypoints;
        }

        public void Init() { }

        public IWorldPoint GetSpawnPoint(Team team)
        {
            IWorldPoint[] points = _teamSpawnPoints.GetValueOrDefault(team);
            return points?.FirstOrDefault();
        }

        public IWorldPoint[] GetAIWaypoints()
        {
            return _aiWaypoints;
        }
    }
}