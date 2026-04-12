using Game.Core.Game;
using Game.Core.Input;
using Game.Core.Ship;
using Game.Infrastructure.Game;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Ship
{
    public class ShipControllerService : ITickable
    {
        public ShipModel Ship { get; private set; }

        private IShipInput _input;
        private ShipFactory _factory;
        private GameStateService _gameStateService;
        private SignalBus _signalBus;

        public ShipControllerService(
            IShipInput input, 
            ShipFactory factory,
            GameStateService gameStateService,
            SignalBus signalBus)
        {
            _input = input;
            _factory = factory;
            _gameStateService = gameStateService;
            _signalBus = signalBus;
        }

        public void CreateIfNeeded()
        {
            if (Ship != null) return;
            Ship = _factory.Create(Vector2.zero);

            _signalBus.Fire<ShipChangedSignal>();
        }

        public void Tick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            if (Ship == null) return;

            var dt = Time.deltaTime;
            Ship.Tick(dt, _input.Thrust, _input.Turn);
        }

        public void ResetShip()
        {
            if (Ship != null)
            {
                Ship.Entity.IsActive = false;
            }

            Ship = _factory.Create(Vector2.zero);
            _signalBus.Fire<ShipChangedSignal>();
        }
    }
}