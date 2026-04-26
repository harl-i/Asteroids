using System;
using Game.Core.Services;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class GameOverHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private IAdService _adService;
        private IFirebaseAnalyticsService _analytics;

        public GameOverHandler(
            SignalBus signalBus,
            IAdService adService,
            IFirebaseAnalyticsService analytics)
        {
            _signalBus = signalBus;
            _adService = adService;
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
            _analytics.LogEvent("game_over");

            _adService.ShowInterstitial();
        }
    }
}