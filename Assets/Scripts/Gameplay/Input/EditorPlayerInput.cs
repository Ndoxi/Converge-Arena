using TowerDefence.Core;
using UnityEngine;

namespace TowerDefence.Gameplay.Input
{
#if UNITY_EDITOR
    public class EditorPlayerInput : MonoBehaviour
    {
        private PlayerInputProxy _proxy;

        private void Awake()
        {
            _proxy = Services.Get<PlayerInputProxy>();
        }

        private void Update()
        {
            var horizontal = UnityEngine.Input.GetAxis("Horizontal");
            var vertical = UnityEngine.Input.GetAxis("Vertical");

            _proxy.SetMove(new Vector2(horizontal, vertical));

            if (UnityEngine.Input.GetKeyUp(KeyCode.J))
                _proxy.SetAttack();
        }
    }
#endif
}

