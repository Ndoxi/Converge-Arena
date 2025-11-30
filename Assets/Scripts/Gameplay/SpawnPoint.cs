using UnityEngine;

namespace TowerDefence.Gameplay
{
    public class SpawnPoint : MonoBehaviour
    {
        public Vector3 position => transform.position;
        public Quaternion rotation => transform.rotation;
    }
}
