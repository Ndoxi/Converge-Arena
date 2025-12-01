using TowerDefence.Core;
using TowerDefence.Game;
using UnityEngine;

namespace TowerDefence.UI
{
    public class PauseScreenMediator : MonoBehaviour
    {
        [SerializeField] private PauseScreen _pauseScreen;
        private IEventBus _eventBus;

        private void Awake()
        {
            _eventBus = Services.Get<IEventBus>();
        }

        private void OnEnable()
        {
            _pauseScreen.resumeRequested += OnResume;
            _pauseScreen.returnToMenuRequested += OnQuit;
        }

        private void OnDisable()
        {
            _pauseScreen.resumeRequested -= OnResume;
            _pauseScreen.returnToMenuRequested -= OnQuit;
        }

        private void OnResume()
        {
            _eventBus.Publish(new ResumeGameRequestedEvent());
        }

        private void OnQuit()
        {
            _eventBus.Publish(new ReturnToMenuRequestedEvent());
        }
    }
}
