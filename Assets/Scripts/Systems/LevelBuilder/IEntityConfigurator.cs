using TowerDefence.Core;
using TowerDefence.Gameplay;

namespace TowerDefence.Systems
{
    public interface IEntityConfigurator : IService
    {
        void Configure(Entity entity);
        void ConfigurePlayer(Entity entity);
        void Clear();
    }
}

