using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.AI;

namespace TowerDefence.Gameplay.Systems
{
    public interface IEntityGroupSystem : IService, IDisposable
    {
        IEntityGroup GetOrCreateGroup(Team team);
    }
}

