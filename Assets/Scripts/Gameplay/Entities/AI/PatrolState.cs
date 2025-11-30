using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using UnityEngine;

namespace TowerDefence.Gameplay.AI
{
    public class PatrolState : IState
    {
        public event Action<Vector3> directionChanged;

        private readonly AIBrainCommandCenter _brain;
        private readonly Entity _entity;
        private Vector3 _waypointPosition;
        private readonly Steering _steering;

        public PatrolState(AIBrainCommandCenter brain, Entity entity)
        {
            _brain = brain;
            _entity = entity;
            _steering = new Steering();
        }

        public void OnEnter(IStateContext context = null)
        {
            if (context is PatrolStateTargetContext target)
                _waypointPosition = target.position;

            _steering.Reset();
        }

        public void OnExit() { }

        public void Tick(float deltaTime)
        {
            var dir = _steering.CalculateDirection(_entity, _brain.activeGroup, _waypointPosition, deltaTime);
            directionChanged?.Invoke(dir);
        }
    }

    public class IdleState: IState
    {
        public void OnEnter(IStateContext context = null) { }
        public void OnExit() { }
        public void Tick(float deltaTime) { }
    }

    public class ChaseState : IState
    {
        private readonly AIBrainCommandCenter _brain;

        public ChaseState(AIBrainCommandCenter brain)
        {
            _brain = brain;
        }

        public void OnEnter(IStateContext context = null) { }
        public void OnExit() { }
        public void Tick(float deltaTime) { }
    }
}

