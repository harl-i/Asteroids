using Game.Core.Physics;
using Game.Core.Ship;
using Game.Infrastructure.Physics;
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

            float acceleration = _config.PlayerConfig.acceleration;
            ShipModel ship = new ShipModel(entity, acceleration, 180f);

            return ship;
        }
    }
}