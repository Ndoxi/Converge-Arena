using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class PauseScreen : BaseScreen
    {
        public event Action resumeRequested;
        public event Action returnToMenuRequested;

        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _returnToMenu;

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeRequested);
            _returnToMenu.onClick.AddListener(OnReturnToMenuRequested);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(OnResumeRequested);
            _returnToMenu.onClick.RemoveListener(OnReturnToMenuRequested);
        }

        private void OnResumeRequested()
        {
            resumeRequested?.Invoke();
        }

        private void OnReturnToMenuRequested() 
        {
            returnToMenuRequested?.Invoke();
        }
    }
}
