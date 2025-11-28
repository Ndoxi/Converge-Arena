using System;
using UnityEngine;

namespace TowerDefence.Gameplay.Input
{
    public class PlayerInputProxy : IPlayerInput
    {
        public event Action<Vector2> moveAction;
        public event Action attackAction;

        public void Init() { }

        public void SetMove(Vector2 value)
        {
            moveAction?.Invoke(value);
        }

        public void SetAttack()
        {
            attackAction?.Invoke();
        }
    }
}

