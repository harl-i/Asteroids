using Game.Core.Enemy;
using Game.Core.Physics;
using Game.Infrastructure.Physics;
using UnityEngine;

namespace Game.Infrastructure.Enemy
{
    public class UfoFactory
    {
        private PhysicsWorldProvider _world;
        private EnemyRegistry _enemyRegistry;

        private float _radius = 1.4f;
        private float _mass = 1.5f;

        public UfoFactory(
            PhysicsWorldProvider world,
            EnemyRegistry enemyRegistry)
        {
            _world = world;
            _enemyRegistry = enemyRegistry;
        }

        public UfoModel Create(Vector2 position)
        {
            Physics2DEntity entity = _world.World.CreateEntity(position, _radius, _mass);
            entity.ConfigureCollision(CollisionLayer.Enemy, 1f);

            UfoModel ufo = new UfoModel(entity);
            entity.SetPhysicsOwner(ufo);

            _enemyRegistry.Add(ufo);
            return ufo;
        }
    }
}
