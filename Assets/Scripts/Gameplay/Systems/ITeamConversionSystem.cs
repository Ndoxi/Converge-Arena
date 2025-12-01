using TowerDefence.Core;

namespace TowerDefence.Gameplay.Systems
{
    public interface ITeamConversionSystem : IService
    {
        public delegate void EntityConversionHandler(IEntity entity, Team oldTeam, Team newTeam);

        event EntityConversionHandler entityConverted;
        void Register(IEntity entity);
        void Unregister(IEntity entity);
        void Clear();
    }
}

