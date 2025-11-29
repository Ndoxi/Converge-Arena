namespace TowerDefence.Core
{
    public class FactoryService : IService
    {
        public GameplayFactory gameplay { get; private set; }

        public void Init()
        {
            gameplay = new GameplayFactory();
            gameplay.Init();
        }
    }
}
