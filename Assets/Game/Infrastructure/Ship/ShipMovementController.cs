using Game.Core.Game;
using Game.Core.Input;
using Game.Core.Ship;
using Game.Infrastructure.Game;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Ship
{
    public class ShipMovementController : ITickable
    {
        private IShipInput _input;
        private ShipRepository _shipRepository;
        private GameStateMachine _gameStateMachine;

        public ShipMovementController(
            IShipInput input,
            ShipRepository shipRepository,
            GameStateMachine gameStateMachine)
        {
            _input = input;
            _shipRepository = shipRepository;
            _gameStateMachine = gameStateMachine;
        }

        public void Tick()
        {
            if (_gameStateMachine.CurrentState != GameState.Playing)
                return;

            ShipModel ship = _shipRepository.Ship;
            if (ship == null) return;

            ship.Tick(Time.deltaTime, _input.Thrust, _input.Turn);
        }
    }
}
