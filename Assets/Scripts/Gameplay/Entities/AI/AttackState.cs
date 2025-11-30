using TowerDefence.Core;
using TowerDefence.Gameplay.Commands;

namespace TowerDefence.Gameplay.AI
{
    public class AttackState : IState
    {
        private readonly AIBrainCommandCenter _brain;
        public AttackState(AIBrainCommandCenter brain)
        {
            _brain = brain;
        }
        public void OnEnter(IStateContext context = null) { }
        public void OnExit() { }
        public void Tick(float deltaTime) { }
    }
}

