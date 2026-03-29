using Game.Core.Ship;
using Game.Infrastructure.Ship;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Enemy
{
    public class UfoMovementService : ITickable
    {
        private UfoService _ufoService;
        private ShipControllerService _shipController;

        private float _ufoAcceleration = 1f;
        private float _maxUfoSpeed = 3f;

        public UfoMovementService(
            UfoService ufoService,
            ShipControllerService shipController)
        {
            _ufoService = ufoService;
            _shipController = shipController;
        }

        public void Tick()
        {
            ShipModel ship = _shipController.Ship;
            if (ship == null)
                return;

            foreach (var ufo in _ufoService.Ufos)
            {
                if (!ufo.Entity.IsActive)
                    continue;

                Vector2 toShip = ship.Entity.Position - ufo.Entity.Position;
                if (toShip.sqrMagnitude < 0.001f)
                    continue;

                Vector2 direction = toShip.normalized;
                Vector2 force = direction * (_ufoAcceleration * ufo.Entity.Mass);
                ufo.Entity.AddForce(force);

                if (ufo.Entity.Velocity.magnitude > _maxUfoSpeed)
                {
                    ufo.Entity.Velocity = ufo.Entity.Velocity.normalized * _maxUfoSpeed;
                }
            }
        }
    }
}