using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay.States
{
    public class IdleState : IState
    {
        private readonly IEntity _entity;
        private readonly Rigidbody _rigidbody;
        private readonly ICommandCenter _commandCenter;
        private readonly IAttackSystem _attackSystem;
        private readonly ITickDispatcher _tickDispatcher;

        public IdleState(IEntity entity,
                         Rigidbody rigidbody,
                         ICommandCenter commandCenter,
                         IAttackSystem attackSystem)
        {
            _entity = entity;
            _rigidbody = rigidbody;
            _commandCenter = commandCenter;
            _attackSystem = attackSystem;
            _tickDispatcher = Services.Get<ITickDispatcher>();
        }

        public void OnEnter(IStateContext context = null) 
        {
            _tickDispatcher.Subscribe(HandleMovement, TickType.FixedUpdate);

            _commandCenter.Subscribe<MoveCommand>(OnMoveCommand);
            _commandCenter.Subscribe<AttackCommand>(OnAttackCommand);
        }

        public void OnExit() 
        {
            _tickDispatcher.Unsubscribe(HandleMovement, TickType.FixedUpdate);

            _commandCenter.Unsubscribe<MoveCommand>(OnMoveCommand);
            _commandCenter.Unsubscribe<AttackCommand>(OnAttackCommand);
        }

        public void Tick(float deltaTime) { }

        private void HandleMovement(float dealtaTime)
        {
            _rigidbody.AddForce(-_rigidbody.angularVelocity, ForceMode.VelocityChange);
        }

        private void OnMoveCommand(MoveCommand moveCommand)
        {
            if (moveCommand.move != Vector2.zero)
                _entity.SetState<MoveState>(new CommandStateContext<MoveCommand>(moveCommand));
        }

        private void OnAttackCommand(AttackCommand attackCommand)
        {
            if (_attackSystem.canAttack)
                _entity.SetState<AttackState>(attackCommand);
        }
    }
}
