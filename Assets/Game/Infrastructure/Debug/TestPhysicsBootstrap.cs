using Game.Core.Physics;
using Game.Infrastructure.Physics;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Debug
{
    public sealed class TestPhysicsBootstrap : IInitializable
    {
        private readonly PhysicsWorldProvider _provider;

        public TestPhysicsBootstrap(PhysicsWorldProvider provider)
        {
            _provider = provider;
        }

        public void Initialize()
        {
            Physics2DEntity entity = _provider.World.CreateEntity(new Vector2(490, 0), radius: 10, mass: 1);
            entity.Velocity = new Vector2(30, 0);
        }
    }
}
