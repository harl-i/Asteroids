using System;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class RestartAnalyticsService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private IFirebaseAnalyticsService _analyticsService;

        public RestartAnalyticsService(
            SignalBus signalBus,
            IFirebaseAnalyticsService analyticsService)
        {
            _signalBus = signalBus;
            _analyticsService = analyticsService;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RestartGameSignal>(OnRestart);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RestartGameSignal>(OnRestart);
        }

        private void OnRestart()
        {
            _analyticsService.LogEvent(AnalyticsConstants.Events.GameRestart);
        }
    }
}
