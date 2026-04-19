using Game.Core.Game;
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

            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                _signalBus.Fire<RestartGameSignal>();
            }
        }
    }
}