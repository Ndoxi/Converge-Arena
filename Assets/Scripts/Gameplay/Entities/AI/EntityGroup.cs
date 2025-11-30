using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay.AI
{
    public class EntityGroup : IEntityGroup, IDisposable
    {
        public Vector3 center { get; private set; }

        public List<Entity> entities { get; private set; } = new List<Entity>(16);

        public void Init() { }

        public void Dispose()
        {
            entities.Clear();
        }

        public void Join(Entity entity)
        {
            if (entities.Contains(entity))
                return;
            entities.Add(entity);
        }

        public void Leave(Entity entity)
        {
            entities.Remove(entity);
        }

        public void Update(float deltaTime) 
        {
            center = Vector3.zero;

            if (entities.Count == 0)
                return;

            if (entities.Count == 0) 
                return;

            Vector3 accumulated = Vector3.zero;
            int aliveCount = 0;
            foreach (var e in entities)
            {
                if (!e.isAlive) 
                    continue;
                accumulated += e.transform.position;
                aliveCount++;
            }

            if (aliveCount == 0) 
                return;

            center = accumulated / aliveCount;
        }
    }
}

