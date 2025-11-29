using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;
using TowerDefence.Gameplay.Systems;
using UnityEngine;

namespace TowerDefence.Gameplay.States
{
    public class IdleState : IState
    {
        private readonly IEntity _entity;
        private readonly ICommandCenter _commandCenter;
        private readonly IAttackSystem _attackSystem;

        public IdleState(IEntity entity, ICommandCenter commandCenter, IAttackSystem attackSystem)
        {
            _entity = entity;
            _commandCenter = commandCenter;
            _attackSystem = attackSystem;
        }

        public void OnEnter(IStateContext context = null) 
        {
            _commandCenter.Subscribe<MoveCommand>(OnMoveCommand);
            _commandCenter.Subscribe<AttackCommand>(OnAttackCommand);
        }

        public void OnExit() 
        {
            _commandCenter.Unsubscribe<MoveCommand>(OnMoveCommand);
            _commandCenter.Unsubscribe<AttackCommand>(OnAttackCommand);
        }

        public void Tick(float deltaTime) { }

        private void OnMoveCommand(MoveCommand moveCommand)
        {
            if (moveCommand.move != Vector2.zero)
                _entity.SetState<MoveState>(new CommandStateContext<MoveCommand>(moveCommand));
        }

        private void OnAttackCommand(AttackCommand attackCommand)
        {
            if (_attackSystem.canAttack)
                _entity.SetState<AttackState>();
        }
    }
}
