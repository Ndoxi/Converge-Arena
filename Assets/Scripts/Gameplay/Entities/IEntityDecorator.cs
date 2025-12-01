using System;
using TowerDefence.Core;

namespace TowerDefence.Gameplay
{
    public interface IEntityDecorator : IDisposable
    {
        void Init(Entity entity);
        void Decorate();
    }
}

