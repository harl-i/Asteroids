using Game.Core.Game;
using Game.Core.Input;
using Game.Core.Physics;
using Game.Core.Ship;
using Game.Core.Signals;
using Game.Infrastructure.Game;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Services;
using Game.Infrastructure.Ship;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Weapons
{
    public class LaserShooter : ITickable
    {
        private IShipInput _input;
        private ShipRepository _shipRepository;
        private LaserState _laserState;
        private PhysicsWorldProvider _worldProvider;
        private ConfigRepository _config;
        private SignalBus _signalBus;
        private GameStateMachine _gameStateMachine;

        public LaserShooter(
            IShipInput input,
            ShipRepository shipRepository,
            LaserState laserState,
            PhysicsWorldProvider worldProvider,
            ConfigRepository config,
            SignalBus signalBus,
            GameStateMachine gameStateMachine)
        {
            _input = input;
            _shipRepository = shipRepository;
            _laserState = laserState;
            _worldProvider = worldProvider;
            _config = config;
            _signalBus = signalBus;
            _gameStateMachine = gameStateMachine;
        }

        public void Tick()
        {
            if (!_config.IsLoaded)
                return;

            if (_gameStateMachine.CurrentState != GameState.Playing)
                return;

            ShipModel ship = _shipRepository.Ship;
            if (ship == null || ship.IsControlLocked)
                return;

            if (_input.IsLaserPressed && _laserState.TrySpendCharge())
            {
                FireLaser(ship);
            }
        }

        private void FireLaser(ShipModel ship)
        {
            float rad = ship.RotationDeg * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            Vector2 origin = ship.Entity.Position;
            float range = _config.PlayerConfig.LaserRange;

            var hits = Physics2DRaycaster.RaycastAll(
                _worldProvider.World.Entities,
                origin,
                direction,
                range);

            foreach (var hit in hits)
            {
                if (!hit.Entity.IsActive)
                    continue;

                _signalBus.Fire(new EnemyDeathRequestedSignal
                {
                    Entity = hit.Entity
                });
            }

            Vector2 endPoint = origin + direction.normalized * range;

            _signalBus.Fire(new LaserFiredSignal
            {
                Start = origin,
                End = endPoint
            });
        }
    }
}
