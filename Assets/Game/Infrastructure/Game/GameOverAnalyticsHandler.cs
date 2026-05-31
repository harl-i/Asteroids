using System;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class GameOverAnalyticsHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private IFirebaseAnalyticsService _analytics;

        public GameOverAnalyticsHandler(
            SignalBus signalBus,
            IFirebaseAnalyticsService analytics)
        {
            _signalBus = signalBus;
            _analytics = analytics;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
        }

        private void OnGameOver()
        {
            _analytics.LogEvent(AnalyticsConstants.Events.GameOver);
        }
    }
}
