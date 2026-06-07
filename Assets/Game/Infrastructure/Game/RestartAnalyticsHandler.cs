using System;
using Game.Core.Services;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class RestartAnalyticsHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private IAnalyticsTracker _analyticsTracker;

        public RestartAnalyticsHandler(
            SignalBus signalBus,
            IAnalyticsTracker analyticsTracker)
        {
            _signalBus = signalBus;
            _analyticsTracker = analyticsTracker;
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
            _analyticsTracker.LogEvent(AnalyticsConstants.Events.GameRestart);
        }
    }
}
