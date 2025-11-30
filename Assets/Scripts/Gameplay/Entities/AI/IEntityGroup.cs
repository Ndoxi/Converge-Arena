using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Gameplay.AI
{
    public interface IEntityGroup : IDisposable
    {
        Vector3 center { get; }
        List<Entity> entities { get; }
        void Join(Entity entity);
        void Leave(Entity entity);
        void Update(float deltaTime);
    }
}

