using System;
using UnityEngine;

namespace TowerDefence.Gameplay.Stats
{
    public class Stat
    {
        public event Action<float> onValueUpdated;

        public float minValue { get; private set; }
        public float maxValue { get; private set; }

        public float value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;

                _value = Mathf.Min(value, maxValue);

                if (_value < minValue)
                    _value = minValue;

                onValueUpdated?.Invoke(_value);
            }
        }

        private float _value;

        public Stat(float value, float minValue, float maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            _value = Mathf.Clamp(value, minValue, maxValue);
        }
    }
}
