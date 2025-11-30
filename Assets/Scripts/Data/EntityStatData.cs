using System;
using TowerDefence.Gameplay.Stats;

namespace TowerDefence.Data
{
    [Serializable]
    public struct EntityStatData 
    {
        public StatType statType;
        public float value;
        public float minValue;
        public float maxValue;
    }
}