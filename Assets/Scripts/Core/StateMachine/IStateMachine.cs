namespace TowerDefence.Core
{
    public interface IStateMachine : IService
    {
        IState CurrentState { get; }
        void SetState(IState newState, IStateContext context = null);
        void Tick(float deltaTime);
    }
}
