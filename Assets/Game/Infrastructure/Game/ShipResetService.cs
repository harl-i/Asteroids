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
        private ShipControllerService _shipController;
        private LaserService _laserService;

        public ShipResetService(
            SignalBus signalBus,
            ShipControllerService shipController,
            LaserService laserService)
        {
            _signalBus = signalBus;
            _shipController = shipController;
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
            _shipController.ResetShip();
        }
    }
}
