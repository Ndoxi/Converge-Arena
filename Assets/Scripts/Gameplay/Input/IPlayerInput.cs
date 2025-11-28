using System;
using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay.Input
{
    public interface IPlayerInput : IService
    {
        event Action<Vector2> moveAction;
        event Action attackAction;
    }
}
