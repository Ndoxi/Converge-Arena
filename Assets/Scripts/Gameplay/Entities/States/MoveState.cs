using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.Stats;
using UnityEngine;

namespace TowerDefence.Gameplay.States
{
    public class MoveState : IState
    {
        private readonly IEntity _entity;
        private readonly Rigidbody _rigidbody;
        private readonly ICommandCenter _commandCenter;
        private readonly Stat _moveSpeedStat;
        private Vector2 _direction;

        public MoveState(IEntity entity, Rigidbody rigidbody, ICommandCenter commandCenter, Stat moveSpeedStat)
        {
            _entity = entity;
            _rigidbody = rigidbody;
            _commandCenter = commandCenter;
            _moveSpeedStat = moveSpeedStat;
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
        }

        private void OnMoveCommand(MoveCommand moveCommand)
        {
            _direction = moveCommand.move;

            if (_direction == Vector2.zero)
                _entity.SetState<IdleState>();
        }

        private void OnAttackCommand(AttackCommand attackCommand)
        {
            _entity.SetState<AttackState>();
        }
    }
}
