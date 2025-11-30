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

        public IWorldPoint[] GetWaypoints()
        {
            return _aiWaypoints;
        }

        public IWorldPoint GetNearest(Vector3 position, float minDistance = 1f)
        {
            IWorldPoint nearestPoint = null;
            float nearestSqrDistance = float.MaxValue;
            foreach (var point in _aiWaypoints)
            {
                float sqrDistance = (point.position - position).sqrMagnitude;
                if (sqrDistance < nearestSqrDistance && sqrDistance >= minDistance * minDistance)
                {
                    nearestSqrDistance = sqrDistance;
                    nearestPoint = point;
                }
            }
            return nearestPoint;
        }

        public IWorldPoint GetRandomWaypoint()
        {
            if (_aiWaypoints.Length == 0)
                return null;
            return _aiWaypoints[Random.Range(0, _aiWaypoints.Length)];
        }
    }
}