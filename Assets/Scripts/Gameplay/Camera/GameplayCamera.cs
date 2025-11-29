using Unity.Cinemachine;
using UnityEngine;

namespace TowerDefence.Gameplay.Cameras
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class GameplayCamera : MonoBehaviour
    {
        private CinemachineCamera _cinemachineCamera;

        public void Init()
        {
            _cinemachineCamera = GetComponent<CinemachineCamera>();
        }

        public void Follow(Transform transform)
        {
            _cinemachineCamera.Follow = transform;
        }
    }
}