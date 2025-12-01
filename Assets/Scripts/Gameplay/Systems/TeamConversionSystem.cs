using System.Collections.Generic;
using TowerDefence.Core;
using TowerDefence.Gameplay.States;
using TowerDefence.Systems;

namespace TowerDefence.Gameplay.Systems
{
    public class TeamConversionSystem : ITeamConversionSystem
    {
        private readonly List<IEntity> _managed = new List<IEntity>(64);

        public void Init() { }

        public void Register(IEntity entity)
        {
            entity.died += ConvertEntity;
            _managed.Add(entity);
        }

        public void Unregister(IEntity entity)
        {
            entity.died -= ConvertEntity;
            _managed.Remove(entity);
        }

        public void Clear()
        {
            foreach (var entity in _managed)
            {
                entity.died -= ConvertEntity;
            }
            _managed.Clear();
        }

        private void ConvertEntity(IEntity entity, IEntity attacker)
        {
            entity.team = attacker.team;
            entity.healthSystem.RestoreAll();
            entity.SetIdle();
        }
    }
}

