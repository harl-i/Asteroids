using Game.Core.Input;
using Game.Core.Ship;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Ship
{
    public class ShipControllerService : ITickable
    {
        public ShipModel Ship { get; private set; }

        private readonly IShipInput _input;
        private readonly ShipFactory _factory;

        public ShipControllerService(IShipInput input, ShipFactory factory)
        {
            _input = input;
            _factory = factory;
        }

        public void CreateIfNeeded()
        {
            if (Ship != null) return;
            Ship = _factory.Create(Vector2.zero);
        }

        public void Tick()
        {
            if (Ship == null) return;

            var dt = Time.deltaTime;
            Ship.Tick(dt, _input.Thrust, _input.Turn);
        }
    }
}