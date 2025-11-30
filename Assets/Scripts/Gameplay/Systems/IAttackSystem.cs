using System;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public interface IAttackSystem : IDisposable
    {
        bool canAttack { get; }
        void Attack(float amount, IEntity attacker, IEntity[] targetsBuffer, int targetsCount);
    }
}

