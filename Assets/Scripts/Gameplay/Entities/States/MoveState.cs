using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.Stats;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay.States
{
    public class MoveState : IState
    {
        private readonly IEntity _entity;
        private readonly Rigidbody _rigidbody;
        private readonly ICommandCenter _commandCenter;
        private readonly IAttackSystem _attackSystem;
        private readonly Stat _moveSpeedStat;
        private readonly ITickDispatcher _tickDispatcher;
        private Vector2 _direction;

        public MoveState(IEntity entity,
                         Rigidbody rigidbody,
                         ICommandCenter commandCenter,
                         IAttackSystem attackSystem)
        {
            _entity = entity;
            _rigidbody = rigidbody;
            _commandCenter = commandCenter;
            _attackSystem = attackSystem;
            _moveSpeedStat = _entity.GetStat(StatType.MoveSpeed);
            _tickDispatcher = Services.Get<ITickDispatcher>();
        }

        public void OnEnter(IStateContext context = null) 
        {
            if (context is CommandStateContext<MoveCommand> commandContext)
                _direction = commandContext.command.move;

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

        private void HandleMovement(float deltaTime)
        {
            if (_direction.sqrMagnitude < 0.01f)
            {
                _rigidbody.linearVelocity = Vector2.zero;
                _entity.SetState<IdleState>();
                return;
            }

            Vector2 movementVector2D = _moveSpeedStat.value * _direction;
            var movementVector = new Vector3(movementVector2D.x, 0f, movementVector2D.y);
            _rigidbody.AddForce(movementVector - _rigidbody.linearVelocity, ForceMode.VelocityChange);

            if (_direction.sqrMagnitude > 0.01f)
            {
                Vector3 forward = movementVector.normalized;
                _rigidbody.MoveRotation(Quaternion.LookRotation(forward));
            }
        }

        private void OnMoveCommand(MoveCommand moveCommand)
        {
            _direction = moveCommand.move;
        }

        private void OnAttackCommand(AttackCommand attackCommand)
        {
            if (_attackSystem.canAttack)
                _entity.SetState<AttackState>(attackCommand);
        }
    }
}
