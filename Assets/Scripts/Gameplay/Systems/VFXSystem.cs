using UnityEngine;
using TowerDefence.Core;
using TowerDefence.Data;
using TowerDefence.Data.Constants;

namespace TowerDefence.Gameplay.Systems
{
    public class VFXSystem : IVFXSystem
    {
        private VFXConfig _config;

        public void Init() 
        {
            _config = Services.Get<IConfigProvider>().Get<VFXConfig>(ConfigNames.VFXConfig);
        }

        public void PlayAttackEffect(Vector3 position, float radius)
        {
            VFX vfx = Object.Instantiate(_config.attackPrefab, position, Quaternion.identity);
            vfx.Play(radius);
        }
    }
}

