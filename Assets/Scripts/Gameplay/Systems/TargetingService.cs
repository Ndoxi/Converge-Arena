using System;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public class TargetingService : ITargetingService
    {
        private static readonly Collider[] _overlapBuffer = new Collider[32];

        public void Init() { }

        public int FindTargets(Vector3 position, float radius, IEntity[] results, Predicate<IEntity> query = null)
        {
            int hits = Physics.OverlapSphereNonAlloc(position, radius, _overlapBuffer);
            int count = 0;
            for (int i = 0; i < hits; i++)
            {
                var hit = _overlapBuffer[i];
                if (hit.attachedRigidbody == null
                    || !hit.attachedRigidbody.TryGetComponent(out IEntity entity)
                    || !entity.isAlive
                    || (query != null && !query.Invoke(entity)))
                {
                    continue;
                }
                if (query != null && !query(entity))
                    continue;
                results[count] = entity;
                count++;
            }
            return count;
        }
    }
}

