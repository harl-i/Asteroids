using Game.Core.Physics;
using Game.Core.World;
using Zenject;

namespace Game.Infrastructure.Physics
{
    public class PhysicsWorldProvider : IInitializable
    {
        private ConfigService _config;
        private float halfFactor = 2f;

        public Physics2DWorld World { get; private set; }

        public PhysicsWorldProvider(ConfigService config)
        {
            _config = config;
        }

        public void Initialize()
        {
            float size = _config.WorldConfig.worldSize;
            float half = size / halfFactor;

            WorldBounds bounds = new WorldBounds(-half, half, -half, half);
            World = new Physics2DWorld(bounds);
        }
    }
}