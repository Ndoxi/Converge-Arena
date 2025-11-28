using System;
using System.Linq;
using TowerDefence.Gameplay.Stats;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public class AttackSystem : IAttackSystem, IDisposable
    {
        public bool canAttack => Time.time < _lastAttackTime + _attackCooldown;

        private static readonly Collider[] _overlapBuffer = new Collider[32];

        private IEntity _owner;
        private readonly Stat _attackSpeedStat;
        private float _lastAttackTime;
        private float _attackCooldown;


        public AttackSystem(IEntity owner,
                            StatType attackSpeedStatType)
        {
            _owner = owner;
            _attackSpeedStat = _owner.GetStat(attackSpeedStatType);

            UpdateAttackCD(_attackSpeedStat.value);

            _attackSpeedStat.onValueUpdated += UpdateAttackCD;
        }

        public void Dispose()
        {
            _attackSpeedStat.onValueUpdated -= UpdateAttackCD;
        }

        public int TryAttack(Vector3 position, float radius, IEntity[] results)
        {
            if (!canAttack)
                return 0;

            _lastAttackTime = Time.time;

            int hits = Physics.OverlapSphereNonAlloc(position, radius, _overlapBuffer);
            int count = 0;
            for (int i = 0; i < hits; i++) 
            {
                var hit = _overlapBuffer[i];
                if (hit.attachedRigidbody == null 
                    || !hit.attachedRigidbody.TryGetComponent(out IEntity entity) 
                    || entity.team == _owner.team)
                {
                    continue;
                }

                results[count] = entity;
                count++;
            }

            return count;
        }

        private void UpdateAttackCD(float value)
        {
            _attackCooldown = 1 / value;
        }
    }
}

