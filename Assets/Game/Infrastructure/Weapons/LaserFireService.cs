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
    public class LaserFireService : ITickable
    {
        private IShipInput _input;
        private ShipService _shipService;
        private LaserStateService _laserStateService;
        private PhysicsWorldProvider _worldProvider;
        private ConfigService _config;
        private SignalBus _signalBus;
        private GameStateService _gameStateService;

        public LaserFireService(
            IShipInput input,
            ShipService shipService,
            LaserStateService laserStateService,
            PhysicsWorldProvider worldProvider,
            ConfigService config,
            SignalBus signalBus,
            GameStateService gameStateService)
        {
            _input = input;
            _shipService = shipService;
            _laserStateService = laserStateService;
            _worldProvider = worldProvider;
            _config = config;
            _signalBus = signalBus;
            _gameStateService = gameStateService;
        }

        public void Tick()
        {
            if (!_config.IsLoaded)
                return;

            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            ShipModel ship = _shipService.Ship;
            if (ship == null || ship.IsControlLocked)
                return;

            if (_input.IsLaserPressed && _laserStateService.TrySpendCharge())
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
