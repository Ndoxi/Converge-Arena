using UnityEngine;

namespace TowerDefence.Gameplay.Commands
{
    public struct MoveCommand : ICommand
    {
        public Vector2 move;

        public MoveCommand(Vector2 move)
        {
            this.move = move;
        }
    }
}
