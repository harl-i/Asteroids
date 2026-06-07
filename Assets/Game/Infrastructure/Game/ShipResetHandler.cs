using System;
using Game.Core.Signals;
using Game.Infrastructure.Ship;
using Game.Infrastructure.Weapons;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class ShipResetHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private ShipRepository _shipRepository;
        private LaserState _laserState;

        public ShipResetHandler(
            SignalBus signalBus,
            ShipRepository shipRepository,
            LaserState laserState)
        {
            _signalBus = signalBus;
            _shipRepository = shipRepository;
            _laserState = laserState;
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
            _laserState.ResetState();
            _shipRepository.ResetShip();
        }
    }
}
