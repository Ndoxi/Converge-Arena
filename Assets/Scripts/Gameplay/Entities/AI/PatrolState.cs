using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
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

        public PatrolState(AIBrainCommandCenter brain, Entity entity)
        {
            _brain = brain;
            _entity = entity;
            _steering = new GroupAwareSteering();
            _worldPoints = Services.Get<IWorldPointsService>();
        }

        public void OnEnter(IStateContext context = null)
        {
            _steering.Reset();
        }

        public void OnExit() { }

        public void Tick(float deltaTime)
        {
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
    }
}

