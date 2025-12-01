using TowerDefence.Core;
using TowerDefence.Data;
using TowerDefence.Data.Constants;
using TowerDefence.Gameplay;
using UnityEngine;

namespace TowerDefence.Systems
{
    public class EntityDecorator : IEntityDecorator
    {
        protected DecoratorConfig _config;
        protected Entity _entity;

        public void Init(Entity entity)
        {
            _config = Services.Get<IConfigProvider>().Get<DecoratorConfig>(ConfigNames.DecoratorConfig);
            _entity = entity;

            _entity.teamChanged += OnTeamChanged;
        }

        public virtual void Decorate()
        {
            UpdateMaterial();
        }

        public void Dispose()
        {
            _entity.teamChanged -= OnTeamChanged;
        }

        protected Material GetMaterial(Team team)
        {
            foreach (var decoratorData in _config.teamDecorationDatas)
            {
                if (team == decoratorData.team)
                    return decoratorData.material;
            }

            return null;
        }

        private void UpdateMaterial()
        {
            Material material = GetMaterial(_entity.team);
            _entity.view.material = material;
        }

        protected virtual void Update()
        {
            UpdateMaterial();
        }

        private void OnTeamChanged(Entity entity)
        {
            Update();
        }
    }
}

