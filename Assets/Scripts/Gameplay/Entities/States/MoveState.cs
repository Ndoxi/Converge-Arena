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
        }

        public void OnEnter(IStateContext context = null) 
        {
            if (context is CommandStateContext<MoveCommand> commandContext)
                _direction = commandContext.command.move;

            _commandCenter.Subscribe<MoveCommand>(OnMoveCommand);
            _commandCenter.Subscribe<AttackCommand>(OnAttackCommand);
        }

        public void OnExit() 
        {
            _commandCenter.Unsubscribe<MoveCommand>(OnMoveCommand);
            _commandCenter.Unsubscribe<AttackCommand>(OnAttackCommand);
        }

        public void Tick(float deltaTime) 
        {
            Vector2 movementVector2D = _moveSpeedStat.value * deltaTime * _direction;
            Vector3 movementVector = new (movementVector2D.x, 0f, movementVector2D.y);
            _rigidbody.MovePosition(_rigidbody.position + movementVector);

            if (movementVector != Vector3.zero)
            {
                Vector3 forward = movementVector.normalized;
                _rigidbody.MoveRotation(Quaternion.LookRotation(forward));
            }
        }

        private void OnMoveCommand(MoveCommand moveCommand)
        {
            _direction = moveCommand.move;

            if (_direction == Vector2.zero)
                _entity.SetState<IdleState>();
        }

        private void OnAttackCommand(AttackCommand attackCommand)
        {
            if (_attackSystem.canAttack)
                _entity.SetState<AttackState>();
        }
    }
}
