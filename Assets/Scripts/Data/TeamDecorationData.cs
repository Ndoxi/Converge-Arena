using System;
using TowerDefence.Gameplay;
using UnityEngine;

namespace TowerDefence.Data
{
    [Serializable]
    public struct TeamDecorationData
    {
        public Team team;
        public Material material;
    }
}