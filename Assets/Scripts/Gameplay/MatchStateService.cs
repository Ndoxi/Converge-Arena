namespace TowerDefence.Gameplay
{
    public class MatchStateService : IMatchStateService
    {
        public MatchState matchState { get; private set; }

        public void Init() { }

        public void SetResult(bool playerWon)
        {
            matchState = new MatchState(playerWon);
        }
    }
}