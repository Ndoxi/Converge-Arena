using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay.AI
{
    public class PatrolState : IState
    {
        public event Action<Vector3> directionChanged;

        private readonly AIBrainCommandCenter _brain;
        private readonly Entity _entity;
        private readonly GroupAwareSteering _steering;
        private readonly IWorldPointsService _worldPoints;
        private readonly ITargetingService _targetingService;
        private static readonly Entity[] _buffer = new Entity[32];
        private Stat _visionRange; 

        public PatrolState(AIBrainCommandCenter brain, Entity entity, GroupAwareSteering steering)
        {
            _brain = brain;
            _entity = entity;
            _steering = steering;
            _worldPoints = Services.Get<IWorldPointsService>();
            _targetingService = Services.Get<ITargetingService>();
        }

        public void Init()
        {
            _visionRange = _entity.GetStat(StatType.VisionRange);
        }

        public void OnEnter(IStateContext context = null)
        {
            _steering.Reset();
        }

        public void OnExit() { }

        public void Tick(float deltaTime)
        {
            int count = _targetingService.FindTargets(_entity.transform.position, _visionRange.value, _buffer, QueryTargets);

            if (count > 0)
            {
                directionChanged?.Invoke(Vector3.zero);
                _brain.SetState<ChaseState>();
                return;
            }

            Vector3 goal;

            if (_brain.activeGroup != null)
            {
                goal = _brain.activeGroup.goal;
            }
            else
            {
                var nearest = _worldPoints?.GetNearest(_entity.transform.position);
                if (nearest == null)
                {
                    _steering.Reset();
                    directionChanged?.Invoke(Vector3.zero);
                    return;
                }

                goal = nearest.position;
            }

            var dir = _steering.CalculateDirection(_entity, _brain.activeGroup, goal, deltaTime);
            directionChanged?.Invoke(dir);
        }

        private bool QueryTargets(IEntity entity)
        {
            return _targetingService.IsEnemy(_entity, entity);
        }
    }
}

