using System;
using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using UnityEngine;

namespace TowerDefence.Gameplay.AI
{
    public class PatrolState : IState
    {
        private const float GroupGravityFactor = 0.3f;
        private const float MinGroupDistance = 1.5f;
        private const float SeparationForce = 10f;

        public event Action<Vector3> directionChanged;

        private readonly AIBrainCommandCenter _brain;
        private readonly Entity _entity;
        private Vector3 _waypointPosition;

        public PatrolState(AIBrainCommandCenter brain, Entity entity) 
        {
            _brain = brain;
            _entity = entity;
        }
        
        public void OnEnter(IStateContext context = null) 
        {
            if (context is PatrolStateTargetContext target)
                _waypointPosition = target.position;
        }

        public void OnExit() { }

        public void Tick(float deltaTime) 
        {
            Vector3 target;
            if (_brain.activeGroup != null)
                target = _waypointPosition + _brain.activeGroup.center * GroupGravityFactor;
            else
                target = _waypointPosition;

            Vector3 targetDirection = (target - _entity.transform.position).normalized;
            Vector3 separation = GetSeparationVector();
            Vector3 finalDirection = (targetDirection + separation).normalized;

            directionChanged?.Invoke(finalDirection);
        }

        private Vector3 GetSeparationVector() 
        {
            if (_brain.activeGroup == null)
                return Vector3.zero;

            Vector3 force = Vector3.zero;
            foreach (var entity in _brain.activeGroup.entities)
            {
                if (_entity == entity)
                    continue;

                Vector3 dirAway = _entity.transform.position - entity.transform.position;
                float dist = dirAway.magnitude;
                if (dist > MinGroupDistance)
                    continue;

                float strength = 1f - (dist / MinGroupDistance);
                force += dirAway.normalized * strength;
            }

            return force * SeparationForce;
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

