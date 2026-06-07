using Game.Core.Game;
using Game.Core.Input;
using Game.Core.Ship;
using Game.Infrastructure.Game;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Ship
{
    public class ShipMovementService : ITickable
    {
        private IShipInput _input;
        private ShipService _shipService;
        private GameStateService _gameStateService;

        public ShipMovementService(
            IShipInput input,
            ShipService shipService,
            GameStateService gameStateService)
        {
            _input = input;
            _shipService = shipService;
            _gameStateService = gameStateService;
        }

        public void Tick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            ShipModel ship = _shipService.Ship;
            if (ship == null) return;

            ship.Tick(Time.deltaTime, _input.Thrust, _input.Turn);
        }
    }
}
