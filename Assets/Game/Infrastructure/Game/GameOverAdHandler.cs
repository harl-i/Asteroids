using System;
using Game.Core.Services;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class GameOverAdHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private IAdService _adService;

        public GameOverAdHandler(
            SignalBus signalBus,
            IAdService adService)
        {
            _signalBus = signalBus;
            _adService = adService;
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
            _adService.ShowInterstitial();
        }
    }
}
