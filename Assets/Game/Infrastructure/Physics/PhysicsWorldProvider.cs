using Game.Core.Physics;
using Game.Core.World;
using Game.Infrastructure.Services;
using Zenject;

namespace Game.Infrastructure.Physics
{
    public class PhysicsWorldProvider : IInitializable
    {
        private ConfigService _config;
        private float halfFactor = 2f;
        private ZenjectCollisionEvents _collisionEvents;

        public Physics2DWorld World { get; private set; }

        public PhysicsWorldProvider(ConfigService config, ZenjectCollisionEvents collisionEvents)
        {
            _config = config;
            _collisionEvents = collisionEvents;
        }

        public void Initialize()
        {
            float size = _config.WorldConfig.worldSize;
            float half = size / halfFactor;

            WorldBounds bounds = new WorldBounds(-half, half, -half, half);
            World = new Physics2DWorld(bounds, _collisionEvents);
        }
    }
}