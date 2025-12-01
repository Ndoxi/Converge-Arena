using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay.AI
{
    public class ChaseState : IState
    {
        public event Action<Vector3> directionChanged;

        private readonly AIBrainCommandCenter _brain;
        private readonly Entity _entity;
        private readonly GroupAwareSteering _steering;
        private readonly ITargetingService _targeting;
        private static readonly Entity[] _buffer = new Entity[32];
        private Stat _visionRange;
        private Stat _attackRange;

        public ChaseState(AIBrainCommandCenter brain, Entity entity, GroupAwareSteering steering)
        {
            _brain = brain;
            _entity = entity;
            _steering = steering;
            _targeting = Services.Get<ITargetingService>();
        }

        public void Init()
        {
            _visionRange = _entity.GetStat(StatType.VisionRange);
            _attackRange = _entity.GetStat(StatType.AttackRange);
        }

        public void OnEnter(IStateContext context = null) { }
        public void OnExit() { }

        public void Tick(float deltaTime) 
        {
            int count = _targeting.FindTargets(_entity.transform.position, _visionRange.value, _buffer, QueryTargets);
            if (count == 0)
            {
                directionChanged?.Invoke(Vector3.zero);
                _brain.SetState<PatrolState>();
                return;
            }

            Entity closestTarget = _targeting.FindClosest(_entity.transform.position, _buffer, count);
            var closestDistance = Vector3.Distance(_entity.transform.position, closestTarget.transform.position);
            if (closestDistance <= _attackRange.value)
            {
                _brain.SetState<AttackState>();
                return;
            }

            var chaseDirection = _steering.CalculateDirection(_entity,
                                                              _brain.activeGroup,
                                                              closestTarget.transform.position,
                                                              deltaTime);
            directionChanged?.Invoke(chaseDirection);
        }

        private bool QueryTargets(IEntity target)
        {
            return _targeting.IsEnemy(_entity, target);
        }
    }
}

