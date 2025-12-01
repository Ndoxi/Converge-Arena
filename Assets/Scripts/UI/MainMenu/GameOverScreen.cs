using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.UI
{
    public class GameOverScreen : BaseScreen
    {
        public event Action showed;
        public event Action returnToMenuRequested;

        [SerializeField] private Button _returnToMenu;
        [SerializeField] private RectTransform _wonRoot;
        [SerializeField] private RectTransform _lostRoot;

        protected override void Awake()
        {
            base.Awake();
            _wonRoot.gameObject.SetActive(false);
            _lostRoot.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _returnToMenu.onClick.AddListener(OnReturnToMenuRequested);
        }

        private void OnDisable()
        {
            _returnToMenu.onClick.RemoveListener(OnReturnToMenuRequested);
        }

        public void SetResult(bool playerWon)
        {
            _wonRoot.gameObject.SetActive(playerWon);
            _lostRoot.gameObject.SetActive(!playerWon);
        }

        private void OnReturnToMenuRequested() 
        {
            returnToMenuRequested?.Invoke();
        }

        protected override void OnShow()
        {
            showed?.Invoke();
        }
    }
}