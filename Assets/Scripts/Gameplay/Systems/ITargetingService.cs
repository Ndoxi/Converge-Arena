using System;
using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public interface ITargetingService : IService
    {
        int FindTargets(Vector3 position, float radius, Entity[] results, Predicate<Entity> query = null);
        Entity FindClosest(Vector3 position, Entity[] entities, int count);
        bool IsEnemy(IEntity entityA, IEntity entityB);
    }
}

