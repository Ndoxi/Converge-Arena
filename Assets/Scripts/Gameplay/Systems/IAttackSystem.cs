using System;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public interface IAttackSystem
    {
        int TryAttack(Vector3 position, float radius, IEntity[] results);
    }
}

