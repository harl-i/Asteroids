using System;
using Game.Core.Game;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class GameStateService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;

        public GameState CurrentState { get; private set; } = GameState.Bootstrap;

        public GameStateService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _signalBus.Subscribe<RestartGameSignal>(OnRestart);

            SetState(GameState.Playing);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _signalBus.Unsubscribe<RestartGameSignal>(OnRestart);
        }

        private void OnGameOver()
        {
            SetState(GameState.GameOver);
        }

        private void OnRestart()
        {
            SetState(GameState.Playing);
        }

        private void SetState(GameState state)
        {
            CurrentState = state;

            _signalBus.Fire(new GameStateChangedSignal
            {
                State = state
            });
        }
    }
}