using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay.AI
{
    public class AttackState : IState
    {
        public event Action<Vector2> attack;

        private readonly AIBrainCommandCenter _brain;
        private readonly Entity _entity;
        private readonly ITargetingService _targeting;
        private Stat _attackRange;
        private static readonly Entity[] _buffer = new Entity[32];

        public AttackState(AIBrainCommandCenter brain, Entity entity)
        {
            _brain = brain;
            _entity = entity;
            _targeting = Services.Get<ITargetingService>();
        }

        public void Init() 
        {
            _attackRange = _entity.GetStat(StatType.AttackRange);
        }

        public void OnEnter(IStateContext context = null) 
        {
            int count = _targeting.FindTargets(_entity.transform.position, _attackRange.value, _buffer, QueryTargets);
            if (count == 0)
            {
                _brain.SetState<PatrolState>();
                return;
            }

            Entity closest = _targeting.FindClosest(_entity.transform.position, _buffer, count);
            var direction3D = (closest.transform.position - _entity.transform.position).normalized;
            var direction = new Vector2(direction3D.x, direction3D.z);

            attack?.Invoke(direction);
            _brain.SetState<PatrolState>();
        }

        public void OnExit() { }
        public void Tick(float deltaTime) { }

        private bool QueryTargets(IEntity target)
        {
            return _targeting.IsEnemy(_entity, target);
        }
    }
}

