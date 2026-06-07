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
        private LaserService _laserService;

        public ShipResetService(
            SignalBus signalBus,
            ShipService shipService,
            LaserService laserService)
        {
            _signalBus = signalBus;
            _shipService = shipService;
            _laserService = laserService;
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
            _laserService.ResetState();
            _shipService.ResetShip();
        }
    }
}
