using TowerDefence.Core;
using TowerDefence.Gameplay.Input;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace TowerDefence.Gameplay.Commands
{
    public class PlayerCommandCenter : CommandCenter
    {
        private readonly IPlayerInput _input;
        private Vector2 _moveInput;
        private bool _attackInput;

        public PlayerCommandCenter()
        {
            _input = Services.Get<IPlayerInput>();

            _input.moveAction += OnMoveAction;
            _input.attackAction += OnAttackAction;
        }

        public override void Dispose()
        {
            _input.moveAction -= OnMoveAction;
            _input.attackAction -= OnAttackAction;
        }

        public override void Tick(float deltaTime)
        {
            //Commands priority
            if (_attackInput)
                IssueCommand(new AttackCommand());
            else
                IssueCommand(new MoveCommand(_moveInput));

            ResetFrame();
        }

        private void OnMoveAction(Vector2 value)
        {
            if (_moveInput == Vector2.zero)
                _moveInput = value;
        }

        private void OnAttackAction()
        {
            _attackInput = true;
        }

        private void ResetFrame()
        {
            _moveInput = Vector2.zero;
            _attackInput = false;
        }
    }
}
