using System;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public interface IAttackSystem
    {
        bool canAttack { get; }
        int TryAttack(Vector3 position, float radius, IEntity[] results);
    }
}

