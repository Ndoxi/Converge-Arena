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
        private readonly AttackState _attackState;
        private readonly Dictionary<Type, IState> _states;
        private readonly IEntityGroupSystem _groupSystem;
        private Vector2 _moveDirection;
        private bool _attack;
        private Vector2 _attackDirection;

        public AIBrainCommandCenter(Entity entity)
        {
            _entity = entity;

            var factory = Services.Get<FactoryService>().gameplay;

            _brainStateMachine = factory.CreateEntityStateMachine();
            var steering = new GroupAwareSteering();
            _patrolState = new PatrolState(this, _entity, steering);
            _chaseState = new ChaseState(this, _entity, steering);
            _attackState = new AttackState(this, _entity);
            _states = new Dictionary<Type, IState>()
            {
                { typeof(PatrolState), _patrolState },
                { typeof(ChaseState), _chaseState },
                { typeof(AttackState), _attackState },
            };

            _groupSystem = Services.Get<IEntityGroupSystem>();
        }

        public void Activate()
        {
            UpdateGroup(_entity);

            _entity.teamChanged += UpdateGroup;
            _patrolState.directionChanged += OnDirectionChanged;
            _chaseState.directionChanged += OnDirectionChanged;
            _attackState.attack += OnAttack;

            _brainStateMachine.Init();
            _patrolState.Init();
            _chaseState.Init();
            _attackState.Init();
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
                IssueCommand(new AttackCommand(_attackDirection));
            else
                IssueCommand(new MoveCommand(_moveDirection));

            ResetFrame();
        }

        public override void Dispose()
        {
            _entity.teamChanged -= UpdateGroup;
            _patrolState.directionChanged -= OnDirectionChanged;
            _chaseState.directionChanged -= OnDirectionChanged;
            _attackState.attack -= OnAttack;
        }

        private void UpdateGroup(Entity entity)
        {
            activeGroup?.Leave(entity);
            activeGroup = _groupSystem.GetOrCreateGroup(entity.team);
            activeGroup.Join(entity);
        }

        private void OnDirectionChanged(Vector3 direction)
        {
            _moveDirection = new Vector2(direction.x, direction.z);
        }

        private void OnAttack(Vector2 direction)
        {
            _attack = true;
            _attackDirection = direction;
        }

        private void ResetFrame()
        {
            _moveDirection = Vector2.zero;
            _attack = false;
            _attackDirection = Vector2.zero;
        }
    }
}
