using System.Linq;
using TowerDefence.Gameplay.Stats;
using UnityEngine;

namespace TowerDefence.Gameplay.Systems
{
    public class AttackSystem : IAttackSystem
    {
        public bool canAttack => Time.time > _lastAttackTime + _attackCooldown;

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

        public void Attack(float amount, IEntity attacker, IEntity[] targetsBuffer, int targetsCount)
        {
            if (!canAttack)
                return;
            _lastAttackTime = Time.time;

            if (targetsCount == 0 || targetsBuffer.Length == 0)
                return;

            for (int i = 0; i < targetsCount; i++)
            {
                var target = targetsBuffer[i];
                target.healthSystem.TakeDamage(amount, attacker);
            }
        }

        private void UpdateAttackCD(float value)
        {
            _attackCooldown = 1 / value;
        }
    }
}

