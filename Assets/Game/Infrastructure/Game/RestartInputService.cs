using Game.Core.Game;
using Game.Core.Signals;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class RestartInputService : ITickable
    {
        private SignalBus _signalBus;
        private GameStateService _gameStateService;

        public RestartInputService(
            SignalBus signalBus,
            GameStateService gameStateService)
        {
            _signalBus = signalBus;
            _gameStateService = gameStateService;
        }

        public void Tick()
        {
            if (_gameStateService.CurrentState != GameState.GameOver)
                return;

            bool restartPressed = UnityEngine.Input.GetKeyDown(KeyCode.R);
            bool restartTapped = Application.isMobilePlatform && UnityEngine.Input.touchCount > 0 &&
                                UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began;

            if (restartPressed || restartTapped)
            {
                _signalBus.Fire<RestartGameSignal>();
            }
        }
    }
}