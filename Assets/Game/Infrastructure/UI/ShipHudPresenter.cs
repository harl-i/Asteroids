using System;
using Game.Core.Ship;
using Game.Core.Signals;
using Game.Core.UI;
using Game.Infrastructure.Ship;
using Zenject;

namespace Game.Infrastructure.UI
{
    public class ShipHudPresenter : ITickable, IInitializable, IDisposable
    {
        private ShipControllerService _shipControllerService;
        private ShipHudViewModel _shipHudViewModel;
        private SignalBus _signalBus;

        public ShipHudViewModel ShipHudViewModel => _shipHudViewModel;

        public ShipHudPresenter(
            ShipControllerService shipControllerService,
            SignalBus signalBus)
        {
            _shipControllerService = shipControllerService;
            _shipHudViewModel = new ShipHudViewModel();
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<LaserChargesChangedSignal>(OnLaserChargesChanged);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<LaserChargesChangedSignal>(OnLaserChargesChanged);
        }

        public void Tick()
        {
            ShipModel ship = _shipControllerService.Ship;
            if (ship == null) return;

            _shipHudViewModel.SetPosition(ship.Entity.Position);
            _shipHudViewModel.SetRotation(ship.RotationDeg);
            _shipHudViewModel.SetSpeed(ship.Entity.Velocity.magnitude);
            _shipHudViewModel.SetHealth(ship.CurrentHealth, ship.MaxHealth);
        }

        private void OnLaserChargesChanged(LaserChargesChangedSignal signal)
        {
            _shipHudViewModel.SetLaserState(
                signal.CurrentCharges,
                signal.MaxCharges,
                signal.CooldownRemaining);
        }
    }
}