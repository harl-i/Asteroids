using Game.Core.Input;
using Game.Core.Game;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class RestartInputHandler : ITickable
    {
        private IGameInput _input;
        private SignalBus _signalBus;
        private GameStateMachine _gameStateMachine;

        public RestartInputHandler(
            IGameInput input,
            SignalBus signalBus,
            GameStateMachine gameStateMachine)
        {
            _input = input;
            _signalBus = signalBus;
            _gameStateMachine = gameStateMachine;
        }

        public void Tick()
        {
            if (_gameStateMachine.CurrentState != GameState.GameOver)
                return;

            if (_input.IsRestartPressed)
            {
                _signalBus.Fire<RestartGameSignal>();
            }
        }
    }
}
