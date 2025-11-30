using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class WorldPoint : MonoBehaviour
    {
        public Vector3 position => transform.position;
        public Quaternion rotation => transform.rotation;
    }
}
