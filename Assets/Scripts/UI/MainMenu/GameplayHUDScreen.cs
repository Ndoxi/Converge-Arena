using TowerDefence.Core;
using TowerDefence.Gameplay.Input;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class GameplayHUDScreen : BaseScreen
    {
        [SerializeField] private Joystick _moveInputJoystick;
        [SerializeField] private Button _attackButton;
        private PlayerInputProxy _proxy;

        protected override void Awake()
        {
            base.Awake();

            _proxy = Services.Get<PlayerInputProxy>();
        }

        private void OnEnable()
        {
            _attackButton.onClick.AddListener(OnAttackPressed);
        }

        private void OnDisable()
        {
            _attackButton.onClick.RemoveListener(OnAttackPressed);
        }

        private void OnAttackPressed()
        {
            _proxy.SetAttack();
        }

        private void Update()
        {
            var horizontal = _moveInputJoystick.Horizontal;
            var vertical = _moveInputJoystick.Vertical;
            _proxy.SetMove(new Vector2(horizontal, vertical));
        }
    }
}
