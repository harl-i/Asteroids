using Game.Core.Ship;
using Game.Core.UI;
using Game.Infrastructure.Ship;
using Zenject;

namespace Game.Infrastructure.UI
{
    public class ShipHudPresenter : ITickable
    {
        private ShipControllerService _shipControllerService;
        private ShipHudViewModel _shipHudViewModel;

        public ShipHudViewModel ShipHudViewModel => _shipHudViewModel;

        public ShipHudPresenter(ShipControllerService shipControllerService)
        {
            _shipControllerService = shipControllerService;
            _shipHudViewModel = new ShipHudViewModel();
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
    }
}