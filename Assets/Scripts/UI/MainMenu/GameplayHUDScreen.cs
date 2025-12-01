using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class GameplayHUDScreen : BaseScreen
    {
        public event Action attackPressed;
        public event Action pauseRequested;
        public Joystick moveInputJoystick => _moveInputJoystick;

        [SerializeField] private Joystick _moveInputJoystick;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _pauseButton;

        private void OnEnable()
        {
            _attackButton.onClick.AddListener(OnAttackPressed);
            _pauseButton.onClick.AddListener(OnPauseRequested);
        }

        private void OnDisable()
        {
            _attackButton.onClick.RemoveListener(OnAttackPressed);
            _pauseButton.onClick.RemoveListener(OnPauseRequested);
        }

        private void OnAttackPressed()
        {
            attackPressed?.Invoke();
        }

        private void OnPauseRequested()
        {
            pauseRequested?.Invoke();
        }
    }
}
