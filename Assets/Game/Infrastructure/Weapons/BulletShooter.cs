using Game.Core.Input;
using Game.Core.Ship;
using Game.Core.Weapons;
using Game.Infrastructure.Services;
using Game.Infrastructure.Ship;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Weapons
{
    public class BulletShooter : ITickable
    {
        private IShipInput _shipInput;
        private ShipRepository _shipRepository;
        private BulletRegistry _bulletRegistry;
        private BulletFactory _bulletFactory;
        private BulletPool _bulletPool;
        private ConfigRepository _configRepository;
        private float _shotCooldownRemaining;

        public BulletShooter(
            IShipInput shipInput,
            ShipRepository shipRepository,
            BulletRegistry bulletRegistry,
            BulletFactory bulletFactory,
            BulletPool bulletPool,
            ConfigRepository configRepository)
        {
            _shipInput = shipInput;
            _shipRepository = shipRepository;
            _bulletRegistry = bulletRegistry;
            _bulletFactory = bulletFactory;
            _bulletPool = bulletPool;
            _configRepository = configRepository;
        }

        public void Tick()
        {
            if (!_configRepository.IsLoaded)
                return;

            _shotCooldownRemaining -= Time.deltaTime;

            ShipModel ship = _shipRepository.Ship;
            if (ship == null ||
                ship.IsControlLocked ||
                !_shipInput.IsFirePressed ||
                _shotCooldownRemaining > 0f)
            {
                return;
            }

            Shoot(ship);
            _shotCooldownRemaining = 1f / _configRepository.PlayerConfig.FireRate;
        }

        public void ResetCooldown()
        {
            _shotCooldownRemaining = 0f;
        }

        private void Shoot(ShipModel ship)
        {
            float rad = ship.RotationDeg * Mathf.Deg2Rad;
            Vector2 forward = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            float spawnOffset = ship.Entity.Radius + _configRepository.PlayerConfig.BulletSpawnOffset;
            Vector2 spawnPosition = ship.Entity.Position + forward * spawnOffset;
            Vector2 inheritedVelocity = ship.Entity.Velocity;

            BulletModel bullet = _bulletPool.HasAvailable
                ? _bulletPool.Get()
                : _bulletFactory.Create();

            _bulletFactory.Activate(bullet, spawnPosition, forward, inheritedVelocity);
            _bulletRegistry.Add(bullet);
        }
    }
}
