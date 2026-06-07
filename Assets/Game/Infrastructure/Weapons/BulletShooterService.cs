using Game.Core.Input;
using Game.Core.Ship;
using Game.Core.Weapons;
using Game.Infrastructure.Services;
using Game.Infrastructure.Ship;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Weapons
{
    public class BulletShooterService : ITickable
    {
        private IShipInput _shipInput;
        private ShipService _shipService;
        private BulletService _bulletService;
        private BulletFactory _bulletFactory;
        private BulletPool _bulletPool;
        private ConfigService _configService;
        private float _shotCooldownRemaining;

        public BulletShooterService(
            IShipInput shipInput,
            ShipService shipService,
            BulletService bulletService,
            BulletFactory bulletFactory,
            BulletPool bulletPool,
            ConfigService configService)
        {
            _shipInput = shipInput;
            _shipService = shipService;
            _bulletService = bulletService;
            _bulletFactory = bulletFactory;
            _bulletPool = bulletPool;
            _configService = configService;
        }

        public void Tick()
        {
            if (!_configService.IsLoaded)
                return;

            _shotCooldownRemaining -= Time.deltaTime;

            ShipModel ship = _shipService.Ship;
            if (ship == null ||
                ship.IsControlLocked ||
                !_shipInput.IsFirePressed ||
                _shotCooldownRemaining > 0f)
            {
                return;
            }

            Shoot(ship);
            _shotCooldownRemaining = 1f / _configService.PlayerConfig.FireRate;
        }

        public void ResetCooldown()
        {
            _shotCooldownRemaining = 0f;
        }

        private void Shoot(ShipModel ship)
        {
            float rad = ship.RotationDeg * Mathf.Deg2Rad;
            Vector2 forward = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            float spawnOffset = ship.Entity.Radius + _configService.PlayerConfig.BulletSpawnOffset;
            Vector2 spawnPosition = ship.Entity.Position + forward * spawnOffset;
            Vector2 inheritedVelocity = ship.Entity.Velocity;

            BulletModel bullet = _bulletPool.HasAvailable
                ? _bulletPool.Get()
                : _bulletFactory.Create();

            _bulletFactory.Activate(bullet, spawnPosition, forward, inheritedVelocity);
            _bulletService.Add(bullet);
        }
    }
}
