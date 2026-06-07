using Game.Core.Enemy;
using Game.Core.Physics;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Services;
using UnityEngine;

namespace Game.Infrastructure.Enemy
{
    public class AsteroidFactory
    {
        private PhysicsWorldProvider _world;
        private EnemyRegistry _enemyRegistry;
        private ConfigRepository _config;

        public AsteroidFactory(
            PhysicsWorldProvider world,
            EnemyRegistry enemyRegistry,
            ConfigRepository config)
        {
            _world = world;
            _enemyRegistry = enemyRegistry;
            _config = config;
        }

        public AsteroidModel Create(Vector2 position, AsteroidSize size)
        {
            float radius = GetRadius(size);
            float mass = radius * _config.EnemyConfig.Asteroid.MassFactor;

            Physics2DEntity entity = _world.World.CreateEntity(position, radius, mass);

            entity.ConfigureCollision(CollisionLayer.Enemy, 0.1f);

            AsteroidModel asteroid = new AsteroidModel(entity, size);
            entity.SetPhysicsOwner(asteroid);

            _enemyRegistry.Add(asteroid);
            return asteroid;
        }

        private float GetRadius(AsteroidSize size)
        {
            AsteroidConfig asteroidConfig = _config.EnemyConfig.Asteroid;

            return size switch
            {
                AsteroidSize.Large => asteroidConfig.LargeRadius,
                AsteroidSize.Medium => asteroidConfig.MediumRadius,
                AsteroidSize.Small => asteroidConfig.SmallRadius,
                _ => asteroidConfig.SmallRadius
            };
        }
    }
}
