using TowerDefence.Core;
using TowerDefence.Game;
using TowerDefence.Gameplay.Input;
using UnityEngine;

namespace TowerDefence.UI
{
    public class GameplayHUDScreenMediator : MonoBehaviour
    {
        [SerializeField] private GameplayHUDScreen _gameplayScreen;
        private PlayerInputProxy _proxy;
        private IEventBus _eventBus;

        private void Awake()
        {
            _proxy = Services.Get<PlayerInputProxy>();
            _eventBus = Services.Get<IEventBus>();
        }

        private void OnEnable()
        {
            _gameplayScreen.attackPressed += OnAttackPressed;
            _gameplayScreen.pauseRequested += OnPauseRequested;
        }

        private void OnDisable()
        {
            _gameplayScreen.attackPressed -= OnAttackPressed;
            _gameplayScreen.pauseRequested -= OnPauseRequested;
        }

        private void Update()
        {
            var horizontal = _gameplayScreen.moveInputJoystick.Horizontal;
            var vertical = _gameplayScreen.moveInputJoystick.Vertical;
            _proxy.SetMove(new Vector2(horizontal, vertical));
        }

        private void OnAttackPressed()
        {
            _proxy.SetAttack();
        }

        private void OnPauseRequested()
        {
            _eventBus.Publish(new PauseGameRequestedEvent());
        }
    }
}
