using TowerDefence.Core;
using TowerDefence.Gameplay;

namespace TowerDefence.Systems
{
    public interface IEntityConfigurator : IService
    {
        void Configure(Entity entity, Team team);
        void ConfigurePlayer(Entity entity, Team team);
        void Clear();
    }
}

