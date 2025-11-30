using UnityEngine;

namespace TowerDefence.Gameplay
{
    public interface IWorldPoint
    {
        Vector3 position { get; }
        Quaternion rotation { get; }
    }
}
