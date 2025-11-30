using System;
using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public interface ITargetingService : IService
    {
        int FindTargets(Vector3 position, float radius, IEntity[] results, Predicate<IEntity> query = null);
    }
}

