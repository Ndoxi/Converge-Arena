using System;
using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Gameplay.AI;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay.Commands
{
    public class AIBrainCommandCenter : CommandCenter
    {
        public IEntityGroup activeGroup { get; private set; }

        private readonly Entity _entity;
        private readonly IStateMachine _brainStateMachine;
        private readonly PatrolState _patrolState;
        private readonly ChaseState _chaseState;
        private readonly Dictionary<Type, IState> _states;
        private readonly IEntityGroupSystem _groupSystem;
        private Vector2 _moveDirection;
        private bool _attack;

        public AIBrainCommandCenter(Entity entity)
        {
            _entity = entity;

            var factory = Services.Get<FactoryService>().gameplay;

            _brainStateMachine = factory.CreateEntityStateMachine();
            _patrolState = new PatrolState(this, _entity);
            _chaseState = new ChaseState(this);

            _states = new Dictionary<Type, IState>()
            {
                { typeof(PatrolState), _patrolState },
                { typeof(ChaseState), _chaseState },
            };

            _groupSystem = Services.Get<IEntityGroupSystem>();
        }

        public void Activate()
        {
            UpdateGroup(_entity.team);

            _entity.teamChanged += UpdateGroup;
            _patrolState.directionChanged += OnDirectionChanged;

            _brainStateMachine.Init();
            _patrolState.Init();
            SetState<PatrolState>();
        }

        public void SetState<TState>(IStateContext context = null) where TState : IState
        {
            var state = _states.GetValueOrDefault(typeof(TState));
            _brainStateMachine.SetState(state, context);
        }

        public override void Tick(float deltaTime)
        {
            _brainStateMachine.Tick(deltaTime);

            //Commands priority
            if (_attack)
                IssueCommand(new AttackCommand());
            else
                IssueCommand(new MoveCommand(_moveDirection));
        }

        public override void Dispose()
        {
            _entity.teamChanged -= UpdateGroup;
            _patrolState.directionChanged -= OnDirectionChanged;
        }

        private void UpdateGroup(Team newTeam)
        {
            activeGroup?.Leave(_entity);
            activeGroup = _groupSystem.GetOrCreateGroup(newTeam);
            activeGroup.Join(_entity);
        }

        private void OnDirectionChanged(Vector3 direction)
        {
            _moveDirection = new Vector2(direction.x, direction.z);
        }
    }
}
