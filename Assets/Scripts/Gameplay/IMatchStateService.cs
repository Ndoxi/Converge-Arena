using TowerDefence.Core;

namespace TowerDefence.Gameplay
{
    public interface IMatchStateService : IService
    {
        MatchState matchState { get; }
        void SetResult(bool playerWon);
    }
}