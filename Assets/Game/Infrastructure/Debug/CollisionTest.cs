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
            a.CollisionLayer = CollisionLayer.Enemy;
            a.Restitution = 1f;
            a.Velocity = new Vector2(20, 0);

            Physics2DEntity b = _provider.World.CreateEntity(new Vector2(100, 0), radius: 18f, mass: 3f);
            b.CollisionLayer = CollisionLayer.Enemy;
            b.Restitution = 1f;
            b.Velocity = new Vector2(-20, 0);
        }
    }
}