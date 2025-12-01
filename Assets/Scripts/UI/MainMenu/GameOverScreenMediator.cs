using TowerDefence.Core;
using TowerDefence.Game;
using TowerDefence.Gameplay;
using UnityEngine;

namespace TowerDefence.UI
{
    public class GameOverScreenMediator : MonoBehaviour
    {
        [SerializeField] private GameOverScreen _view;
        private IEventBus _eventBus;
        private IMatchStateService _matchStateService;

        private void Awake()
        {
            _eventBus = Services.Get<IEventBus>();
            _matchStateService = Services.Get<IMatchStateService>();
        }

        private void OnEnable()
        {
            _view.returnToMenuRequested += OnReturnToMenuRequested;
            _view.showed += OnShow;
        }

        private void OnDisable()
        {
            _view.returnToMenuRequested -= OnReturnToMenuRequested;
            _view.showed -= OnShow;
        }

        private void OnReturnToMenuRequested()
        {
            _eventBus.Publish(new ReturnToMenuRequestedEvent());
        }

        private void OnShow()
        {
            MatchState matchState = _matchStateService.matchState;
            if (matchState.isGameOver)
                _view.SetResult(matchState.playerWon);
        }
    }
}
