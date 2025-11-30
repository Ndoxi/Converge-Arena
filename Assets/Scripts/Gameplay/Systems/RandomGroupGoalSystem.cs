using TowerDefence.Core;
using TowerDefence.Gameplay.AI;

namespace TowerDefence.Gameplay.Systems
{
    public class RandomGroupGoalSystem : IGroupGoalSystem
    {
        private const float MinDistanceToGoal = 1f;

        private IWorldPointsService _worldPoints;

        public void Init()
        {
            _worldPoints = Services.Get<IWorldPointsService>();
        }

        public void Update(IEntityGroup group)
        {
            if ((group.goal - group.center).magnitude > MinDistanceToGoal)
                return;

            var randomWaypoint = _worldPoints.GetRandomWaypoint();
            if (randomWaypoint != null)
                group.goal = randomWaypoint.position;
        }
    }
}

