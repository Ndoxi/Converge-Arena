using UnityEngine;

namespace TowerDefence.Gameplay
{
    [RequireComponent(typeof(ParticleSystem))]
    public class VFX : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void Play(float radius)
        {
            ParticleSystem.MainModule main = _particleSystem.main;
            main.startSize = radius * 2;

            _particleSystem.Play();
        }

        private void OnParticleSystemStopped()
        {
            Destroy(gameObject);
        }
    }
}
