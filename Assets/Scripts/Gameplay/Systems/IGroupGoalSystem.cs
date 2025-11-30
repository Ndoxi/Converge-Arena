using TowerDefence.Core;
using TowerDefence.Gameplay.AI;

namespace TowerDefence.Gameplay.Systems
{
    public interface IGroupGoalSystem : IService
    {
        void Update(IEntityGroup group);
    }
}

