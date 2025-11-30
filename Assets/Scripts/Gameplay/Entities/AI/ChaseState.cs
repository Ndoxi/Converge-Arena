using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;

namespace TowerDefence.Gameplay.AI
{
    public class ChaseState : IState
    {
        private readonly AIBrainCommandCenter _brain;

        public ChaseState(AIBrainCommandCenter brain)
        {
            _brain = brain;
        }

        public void OnEnter(IStateContext context = null) { }
        public void OnExit() { }
        public void Tick(float deltaTime) { }
    }
}

