using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay.States
{
    public class AttackState : IState
    {
        private readonly IEntity _entity;
        private readonly Rigidbody _rigidbody;
        private readonly ICommandCenter _commandCenter;
        private readonly IAttackSystem _attackSystem;
        private readonly ITargetingService _targetingService;
        private readonly IVFXSystem _vfxSystem;
        private readonly Stat _attackStat;
        private readonly Stat _attackRangeStat;
        private readonly IEntity[] _buffer = new IEntity[32];

        public AttackState(IEntity entity,
                           Rigidbody rigidbody,
                           ICommandCenter commandCenter,
                           IAttackSystem attackSystem)
        {
            _entity = entity;
            _rigidbody = rigidbody;
            _commandCenter = commandCenter;
            _attackSystem = attackSystem;

            _targetingService = Services.Get<ITargetingService>();
            _vfxSystem = Services.Get<IVFXSystem>();
            _attackStat = _entity.GetStat(StatType.AttackDamage);
            _attackRangeStat = _entity.GetStat(StatType.AttackRange);
        }

        public void OnEnter(IStateContext context = null)
        {
            PerformAttack();
            _entity.SetState<IdleState>();
        }

        public void OnExit() {}

        public void Tick(float deltaTime) { }

        private void PerformAttack()
        {
            if (!_attackSystem.canAttack)
                return;

            var radius = _attackRangeStat.value / 2;
            Vector3 center = _rigidbody.position + _rigidbody.transform.forward * radius;

            int count = _targetingService.FindTargets(center, radius, _buffer, QueryTargets);
            _attackSystem.Attack(_attackStat.value, _entity, _buffer, count);

            _vfxSystem.PlayAttackEffect(center, radius);
        }

        private bool QueryTargets(IEntity entity)
        {
            return entity.team != _entity.team;
        }
    }
}
