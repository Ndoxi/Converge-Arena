using UnityEngine;
using TowerDefence.Core;

namespace TowerDefence.Gameplay.Systems
{
    public interface IVFXSystem : IService
    {
        void PlayAttackEffect(Vector3 position, float radius);
    }
}

