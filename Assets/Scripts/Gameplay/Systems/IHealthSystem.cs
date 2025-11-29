using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

namespace TowerDefence.Gameplay.Systems
{
    public interface IHealthSystem
    {
        delegate void DamageTakenHandler(IEntity attacker);

        event DamageTakenHandler damageTaken;
        event Action died;
        void TakeDamage(float amount, IEntity attacker);
        void RestoreAll();
    }
}

