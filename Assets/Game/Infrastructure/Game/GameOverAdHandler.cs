using System;
using Game.Core.Services;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class GameOverAdHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private IAdProvider _adProvider;

        public GameOverAdHandler(
            SignalBus signalBus,
            IAdProvider adProvider)
        {
            _signalBus = signalBus;
            _adProvider = adProvider;
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
            _adProvider.ShowInterstitial();
        }
    }
}
