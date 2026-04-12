using System.Collections.Generic;
using Game.Core.Input;
using Game.Core.Ship;
using Game.Core.Weapons;
using Game.Infrastructure.Ship;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Weapons
{
    public class BulletService : ITickable
    {
        private IShipInput _shipInput;
        private ShipControllerService _shipController;
        private BulletFactory _bulletFactory;
        private BulletPool _bulletPool;
        private ConfigService _configService;
        private float _shotCooldownRemaining;

        private float _offsetCoefficient = -0.2f;

        private List<BulletModel> _bullets = new List<BulletModel>();

        public IReadOnlyList<BulletModel> Bullets => _bullets;

        public BulletService(
            IShipInput shipInput,
            ShipControllerService shipController,
            BulletFactory bulletFactory,
            BulletPool bulletPool,
            ConfigService configService)
        {
            _shipInput = shipInput;
            _shipController = shipController;
            _bulletFactory = bulletFactory;
            _bulletPool = bulletPool;
            _configService = configService;
        }

        public void Tick()
        {
            _shotCooldownRemaining -= Time.deltaTime;

            ShipModel ship = _shipController.Ship;

            if (ship != null && !ship.IsControlLocked && _shipInput.IsFirePressed)
            {
                Shoot(ship);
                _shotCooldownRemaining = 1f / _configService.PlayerConfig.fireRate;
            }

            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                BulletModel bullet = _bullets[i];
                bullet.Tick(Time.deltaTime);

                if (!bullet.IsAlive)
                {
                    DespawnBullet(bullet);
                    _bullets.RemoveAt(i);
                }
            }
        }

        public void Clear()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                var bullet = _bullets[i];
                bullet.Destroy();
                _bulletPool.Return(bullet);
            }

            _bullets.Clear();
            _shotCooldownRemaining = 0f;
        }

        private void Shoot(ShipModel ship)
        {
            float rad = ship.RotationDeg * Mathf.Deg2Rad;
            Vector2 forward = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            float spawnOffset = ship.Entity.Radius + _offsetCoefficient;
            Vector2 spawnPosition = ship.Entity.Position + forward * spawnOffset;
            Vector2 inheritedVelocity = ship.Entity.Velocity;

            BulletModel bullet = _bulletPool.HasAvailable
                ? _bulletPool.Get()
                : _bulletFactory.Create();

            _bulletFactory.Activate(bullet, spawnPosition, forward, inheritedVelocity);
            _bullets.Add(bullet);
        }

        private void DespawnBullet(BulletModel bullet)
        {
            bullet.Destroy();
            _bulletPool.Return(bullet);
        }
    }
}