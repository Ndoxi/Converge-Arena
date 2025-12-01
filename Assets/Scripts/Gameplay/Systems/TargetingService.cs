using System;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public class TargetingService : ITargetingService
    {
        private static readonly Collider[] _overlapBuffer = new Collider[32];

        public void Init() { }

        public int FindTargets(Vector3 position, float radius, Entity[] results, Predicate<Entity> query = null)
        {
            int hits = Physics.OverlapSphereNonAlloc(position, radius, _overlapBuffer);
            int count = 0;
            for (int i = 0; i < hits; i++)
            {
                var hit = _overlapBuffer[i];
                if (hit.attachedRigidbody == null
                    || !hit.attachedRigidbody.TryGetComponent(out Entity entity)
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

        public Entity FindClosest(Vector3 position, Entity[] entities, int count)
        {
            Entity closest = null;
            float closestDistanceSqr = float.MaxValue;
            for (int i = 0; i < count; i++)
            {
                var entity = entities[i];
                float distanceSqr = (entity.transform.position - position).sqrMagnitude;
                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closest = entity;
                }
            }
            return closest;
        }

        public bool IsEnemy(IEntity entityA, IEntity entityB)
        {
            return entityA.team != entityB.team;
        }
    }
}

