using System;
using TowerDefence.Core;
using TowerDefence.Gameplay;

namespace TowerDefence.Systems
{
    public interface ILevelBuilder : IService
    {
        void Load();
        void Unload();
    }
}

