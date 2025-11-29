using TowerDefence.Core;

namespace TowerDefence.Gameplay.Systems
{
    public interface ITeamConversionSystem : IService
    {
        void Register(IEntity entity);
        void Unregister(IEntity entity);
        void Clear();
    }
}

