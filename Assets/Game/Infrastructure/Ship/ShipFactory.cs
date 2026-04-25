using Game.Core.Physics;
using Game.Core.Ship;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Services;
using UnityEngine;

namespace Game.Infrastructure.Ship
{
    public class ShipFactory
    {
        private PhysicsWorldProvider _worldProvider;
        private ConfigService _config;

        public ShipFactory(PhysicsWorldProvider worldProvider, ConfigService config)
        {
            _worldProvider = worldProvider;
            _config = config;
        }

        public ShipModel Create(Vector2 startPosition)
        {
            float radius = _config.PlayerConfig.radius;
            float mass = _config.PlayerConfig.mass;
            Physics2DEntity entity = _worldProvider.World.CreateEntity(startPosition, radius, mass);
            entity.CollisionLayer = CollisionLayer.Ship;
            entity.Restitution = 1f;

            float acceleration = _config.PlayerConfig.acceleration;
            float turnSpeedDeg = _config.PlayerConfig.turnSpeedDeg;
            int maxHealth = _config.PlayerConfig.maxHealth;
            ShipModel ship = new ShipModel(entity, acceleration, turnSpeedDeg, maxHealth);

            entity.PhysicsOwner = ship;

            return ship;
        }
    }
}