using TowerDefence.Core;
using TowerDefence.Gameplay.Systems;

namespace TowerDefence.Gameplay.States
{
    public class AttackState : IState
    {
        private readonly IAttackSystem _attackSystem;

        public AttackState(IAttackSystem attackSystem)
        {
            _attackSystem = attackSystem;
        }

        public void OnEnter(IStateContext context = null) { }
        public void OnExit() { }
        public void Tick(float deltaTime) { }
    }
}
