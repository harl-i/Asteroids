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

        private float _offsetCoefficient = 2f;

        private List<BulletModel> _bullets = new List<BulletModel>();

        public IReadOnlyList<BulletModel> Bullets => _bullets;

        public BulletService(
            IShipInput shipInput,
            ShipControllerService shipController,
            BulletFactory bulletFactory)
        {
            _shipInput = shipInput;
            _shipController = shipController;
            _bulletFactory = bulletFactory;
        }

        public void Tick()
        {
            ShipModel ship = _shipController.Ship;

            if (ship != null && !ship.IsControlLocked && _shipInput.IsFirePressed)
                Shoot(ship);

            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                var bullet = _bullets[i];
                bullet.Tick(Time.deltaTime);

                if (!bullet.IsAlive)
                {
                    _bullets.RemoveAt(i);
                }
            }
        }

        private void Shoot(ShipModel ship)
        {
            float rad = ship.RotationDeg * Mathf.Deg2Rad;
            Vector2 forward = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            float spawnOffset = ship.Entity.Radius + _offsetCoefficient;
            Vector2 spawnPosition = ship.Entity.Position + forward * spawnOffset;
            Vector2 inheritedVelocity = ship.Entity.Velocity;

            BulletModel bullet = _bulletFactory.Create(spawnPosition, forward, inheritedVelocity);
            _bullets.Add(bullet);
        }
    }
}