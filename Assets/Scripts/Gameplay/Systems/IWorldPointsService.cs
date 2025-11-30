using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public interface IWorldPointsService : IService
    {
        IWorldPoint GetSpawnPoint(Team team);
        IWorldPoint[] GetWaypoints();
        IWorldPoint GetNearest(Vector3 position, float minDistance = 1f);
        IWorldPoint GetRandomWaypoint();
    }
}