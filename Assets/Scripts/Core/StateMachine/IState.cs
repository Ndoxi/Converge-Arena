namespace TowerDefence.Core
{
    public interface IState
    {
        void OnEnter(IStateContext context = null);
        void OnExit();
        void Tick(float deltaTime);
    }

    public interface IStateContext { }
}

