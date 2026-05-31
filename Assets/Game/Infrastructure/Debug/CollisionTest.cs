using Game.Core.Physics;
using Game.Infrastructure.Physics;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Debug
{
    public class CollisionTest : IInitializable
    {
        private readonly PhysicsWorldProvider _provider;

        public CollisionTest(PhysicsWorldProvider provider)
        {
            _provider = provider;
        }

        public void Initialize()
        {
            Physics2DEntity a = _provider.World.CreateEntity(new Vector2(-100, 0), radius: 18f, mass: 3f);
            a.ConfigureCollision(CollisionLayer.Enemy, 1f);
            a.SetVelocity(new Vector2(20, 0));

            Physics2DEntity b = _provider.World.CreateEntity(new Vector2(100, 0), radius: 18f, mass: 3f);
            b.ConfigureCollision(CollisionLayer.Enemy, 1f);
            b.SetVelocity(new Vector2(-20, 0));
        }
    }
}
