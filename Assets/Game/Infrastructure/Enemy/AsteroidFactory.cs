using Game.Core.Enemy;
using Game.Core.Physics;
using Game.Infrastructure.Physics;
using UnityEngine;

namespace Game.Infrastructure.Enemy
{
    public class AsteroidFactory
    {
        private PhysicsWorldProvider _world;
        private EnemyService _enemyService;

        private float _largeSize = 1.3f;
        private float _mediumSize = 0.7f;
        private float _smallSize = 0.4f;
        private float _massFactor = 0.5f;

        public AsteroidFactory(PhysicsWorldProvider world, EnemyService enemyService)
        {
            _world = world;
            _enemyService = enemyService;
        }

        public AsteroidModel Create(Vector2 position, AsteroidSize size)
        {
            float radius = size switch
            {
                AsteroidSize.Large => _largeSize,
                AsteroidSize.Medium => _mediumSize,
                AsteroidSize.Small => _smallSize,
                _ => _smallSize
            };

            float mass = radius * _massFactor;

            Physics2DEntity entity = _world.World.CreateEntity(position, radius, mass);

            entity.CollisionLayer = CollisionLayer.Enemy;
            entity.Restitution = 0.1f;

            AsteroidModel asteroid = new AsteroidModel(entity, size);
            entity.PhysicsOwner = asteroid;

            _enemyService.Add(asteroid);
            return asteroid;
        }
    }
}