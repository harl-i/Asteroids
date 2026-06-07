using System;
using Game.Core.Signals;
using Game.Infrastructure.Ship;
using Game.Infrastructure.Weapons;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class ShipResetService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private ShipService _shipService;
        private LaserStateService _laserStateService;

        public ShipResetService(
            SignalBus signalBus,
            ShipService shipService,
            LaserStateService laserStateService)
        {
            _signalBus = signalBus;
            _shipService = shipService;
            _laserStateService = laserStateService;
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
            _laserStateService.ResetState();
            _shipService.ResetShip();
        }
    }
}
