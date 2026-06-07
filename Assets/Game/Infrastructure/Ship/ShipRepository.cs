using Game.Core.Ship;
using Game.Core.Signals;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Ship
{
    public class ShipRepository
    {
        private ShipFactory _factory;
        private SignalBus _signalBus;

        public ShipModel Ship { get; private set; }

        public ShipRepository(
            ShipFactory factory,
            SignalBus signalBus)
        {
            _factory = factory;
            _signalBus = signalBus;
        }

        public void CreateIfNeeded()
        {
            if (Ship != null) return;

            Ship = _factory.Create(Vector2.zero);
            _signalBus.Fire<ShipChangedSignal>();
        }

        public void ResetShip()
        {
            if (Ship != null)
            {
                Ship.Entity.SetActive(false);
            }

            Ship = _factory.Create(Vector2.zero);
            _signalBus.Fire<ShipChangedSignal>();
        }
    }
}
