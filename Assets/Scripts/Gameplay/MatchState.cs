namespace TowerDefence.Gameplay
{
    public struct MatchState
    {
        public bool isGameOver;
        public bool playerWon;

        public MatchState(bool playerWon)
        {
            isGameOver = true;
            this.playerWon = playerWon;
        }
    }
}