using System;
using TowerDefence.Core;

namespace TowerDefence.Gameplay.States
{
    public class DeathState : IState
    {
        public event Action entered;
        public event Action exited;

        public void OnEnter(IStateContext context = null) 
        {
            entered?.Invoke();
        }

        public void OnExit() 
        {
            exited?.Invoke();
        }

        public void Tick(float deltaTime) { }
    }
}
