namespace TowerDefence.Core
{
    public class FactoryService : IService
    {
        public GameplayServicesFactory gameplay { get; private set; }

        public void Init()
        {
            gameplay = new GameplayServicesFactory();
            gameplay.Init();
        }
    }
}
