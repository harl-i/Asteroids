using Game.Core.Input;
using Game.Core.Game;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class RestartInputService : ITickable
    {
        private IGameInput _input;
        private SignalBus _signalBus;
        private GameStateService _gameStateService;

        public RestartInputService(
            IGameInput input,
            SignalBus signalBus,
            GameStateService gameStateService)
        {
            _input = input;
            _signalBus = signalBus;
            _gameStateService = gameStateService;
        }

        public void Tick()
        {
            if (_gameStateService.CurrentState != GameState.GameOver)
                return;

            if (_input.IsRestartPressed)
            {
                _signalBus.Fire<RestartGameSignal>();
            }
        }
    }
}
