using Game.Core.Game;
using Game.Core.Input;
using Game.Core.Physics;
using Game.Core.Ship;
using Game.Core.Signals;
using Game.Infrastructure.Game;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Ship;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Weapons
{
    public class LaserService : ITickable, IInitializable
    {
        private IShipInput _input;
        private ShipControllerService _shipController;
        private PhysicsWorldProvider _worldProvider;
        private ConfigService _config;
        private SignalBus _signalBus;
        private GameStateService _gameStateService;

        private int _currentCharges;
        private int _maxCharges;
        private float _cooldownSeconds;
        private float _cooldownRemaining;

        public int CurrentCharges => _currentCharges;
        public int MaxCharges => _maxCharges;
        public float CooldownRemaining => _cooldownRemaining;

        public LaserService(
            IShipInput input,
            ShipControllerService shipController,
            PhysicsWorldProvider worldProvider,
            ConfigService config,
            SignalBus signalBus,
            GameStateService gameStateService)
        {
            _input = input;
            _shipController = shipController;
            _worldProvider = worldProvider;
            _config = config;
            _signalBus = signalBus;
            _gameStateService = gameStateService;
        }

        public void Initialize()
        {
            _maxCharges = _config.PlayerConfig.laserCharges;
            _currentCharges = _maxCharges;
            _cooldownSeconds = _config.PlayerConfig.laserCooldown;

            PublishState();
        }

        public void Tick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            RechargeTick();

            ShipModel ship = _shipController.Ship;
            if (ship == null || ship.IsControlLocked)
                return;

            if (_input.IsLaserPressed && _currentCharges > 0)
            {
                FireLaser(ship);
            }
        }

        public void ResetState()
        {
            _maxCharges = _config.PlayerConfig.laserCharges;
            _currentCharges = _maxCharges;
            _cooldownSeconds = _config.PlayerConfig.laserCooldown;
            _cooldownRemaining = 0f;

            PublishState();
        }

        private void RechargeTick()
        {
            if (_currentCharges >= _maxCharges)
                return;

            _cooldownRemaining -= Time.deltaTime;

            if (_cooldownRemaining <= 0f)
            {
                _currentCharges++;
                if (_currentCharges < _maxCharges)
                {
                    _cooldownRemaining = _cooldownSeconds;
                }
                else
                {
                    _cooldownRemaining = 0f;
                }

                PublishState();
            }
        }

        private void FireLaser(ShipModel ship)
        {
            _currentCharges--;

            if (_currentCharges < _maxCharges && _cooldownRemaining <= 0f)
            {
                _cooldownRemaining = _cooldownSeconds;
            }

            float rad = ship.RotationDeg * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            Vector2 origin = ship.Entity.Position;
            float range = _config.PlayerConfig.laserRange;

            var hits = Physics2DRaycaster.RaycastAll(
                _worldProvider.World.Entities,
                origin,
                direction,
                range);

            //foreach (var hit in hits)
            //{
            //    if (!hit.Entity.IsActive)
            //        continue;

            //    if (hit.Entity.PhysicsOwner is AsteroidModel asteroid)
            //    {
            //        asteroid.Destroy();

            //        _signalBus.Fire(new EnemyKilledSignal
            //        {
            //            Type = asteroid.EnemyType
            //        });
            //    }
            //    else
            //    {
            //        hit.Entity.IsActive = false;
            //    }
            //}
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

            PublishState();
        }

        private void PublishState()
        {
            _signalBus.Fire(new LaserChargesChangedSignal
            {
                CurrentCharges = _currentCharges,
                MaxCharges = _maxCharges,
                CooldownRemaining = _cooldownRemaining
            });
        }
    }
}