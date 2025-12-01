
using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay.Commands
{
    public struct AttackCommand : ICommand, IStateContext
    {
        public Vector2 direction;

        public AttackCommand(Vector2 direction)
        {
            this.direction = direction;
        }
    }
}
