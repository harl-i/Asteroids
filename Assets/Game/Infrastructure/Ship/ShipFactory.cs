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
        private float _radius;
        private float _mass;

        public ShipFactory(PhysicsWorldProvider worldProvider, ConfigService config)
        {
            _worldProvider = worldProvider;
            _config = config;
            _radius = _config.PlayerConfig.radius;
            _mass = _config.PlayerConfig.mass;
        }

        public ShipModel Create(Vector2 startPosition)
        {
            Physics2DEntity entity = _worldProvider.World.CreateEntity(startPosition, _radius, _mass);

            float acceleration = _config.PlayerConfig.acceleration;
            ShipModel ship = new ShipModel(entity, acceleration, 180f);

            return ship;
        }
    }
}