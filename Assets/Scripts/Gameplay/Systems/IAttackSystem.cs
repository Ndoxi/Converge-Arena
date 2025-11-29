using System;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public interface IAttackSystem
    {
        bool canAttack { get; }
        int FindTargets(Vector3 position, float radius, IEntity[] results);
        void ApplyDamage(float amount, IEntity attacker, IEntity[] targetsBuffer, int targetsCount);
    }
}

