using System;
using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Gameplay.AI;

namespace TowerDefence.Gameplay.Systems
{
    public class EntityGroupSystem : IEntityGroupSystem
    {
        private const TickType GroupTickType = TickType.FixedUpdate;

        private readonly Dictionary<Team, IEntityGroup> _groups = new Dictionary<Team, IEntityGroup>();
        private IGroupGoalSystem _groupGoalSystem;
        private ITickDispatcher _tickDispatcher;

        public void Init() 
        {
            _groupGoalSystem = Services.Get<IGroupGoalSystem>();
            _tickDispatcher = Services.Get<ITickDispatcher>();
            _tickDispatcher.Subscribe(Update, GroupTickType);
        }

        public void Dispose() 
        {
            _tickDispatcher.Unsubscribe(Update, GroupTickType);

            foreach (var pair in _groups)
                pair.Value.Dispose();

            _groups.Clear();
        }

        public IEntityGroup GetOrCreateGroup(Team team)
        {
            if (_groups.TryGetValue(team, out IEntityGroup group))
                return group;

            var newGroup = new EntityGroup();
            newGroup.Init();
            _groupGoalSystem.Update(newGroup);
            _groups.Add(team, newGroup);

            return newGroup;
        }

        private void Update(float deltaTime)
        {
            foreach (var pair in _groups)
            {
                IEntityGroup group = pair.Value;
                group.Update(deltaTime);
                _groupGoalSystem.Update(group);
            }
        }
    }
}

