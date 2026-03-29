using Game.Core.Enemy;
using Game.Core.World;
using Game.Infrastructure.Physics;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Enemy
{
    public class AsteroidSpawnService : ITickable
    {
        private AsteroidFactory _factory;
        private AsteroidService _asteroidService;
        private PhysicsWorldProvider _worldProvider;
        private ConfigService _config;

        private float _spawnTimer;

        public AsteroidSpawnService(
            AsteroidFactory factory,
            AsteroidService asteroidService,
            PhysicsWorldProvider worldProvider,
            ConfigService config)
        {
            _factory = factory;
            _asteroidService = asteroidService;
            _worldProvider = worldProvider;
            _config = config;
        }

        public void Tick()
        {
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer > 0f)
                return;

            _spawnTimer = _config.EnemyConfig.asteroidSpawnInterval;

            if (_asteroidService.ActiveCount >= _config.WorldConfig.maxEnemies)
                return;

            SpawnLargeAsteroid();
        }

        private void SpawnLargeAsteroid()
        {
            WorldBounds bounds = _worldProvider.World.Bounds;
            Vector2 spawnPosition = GetSpawnPositionOutside(bounds);
            AsteroidModel asteroid = _factory.Create(spawnPosition, AsteroidSize.Large);

            Vector2 targetDirection = (Vector2.zero - spawnPosition).normalized;
            Vector2 randomSpread = Random.insideUnitCircle * 0.35f;
            Vector2 direction = (targetDirection + randomSpread).normalized;

            float speed = Random.Range(
                _config.EnemyConfig.asteroidMinSpeed,
                _config.EnemyConfig.asteroidMaxSpeed);

            asteroid.Entity.Velocity = direction * speed;
        }

        private Vector2 GetSpawnPositionOutside(WorldBounds bounds)
        {
            float padding = 40f;
            int side = Random.Range(0, 4);

            return side switch
            {
                0 => new Vector2(Random.Range(bounds.MinX, bounds.MaxX), bounds.MaxY + padding),
                1 => new Vector2(Random.Range(bounds.MinX, bounds.MaxX), bounds.MinY - padding),
                2 => new Vector2(bounds.MinX - padding, Random.Range(bounds.MinY, bounds.MaxY)),
                _ => new Vector2(bounds.MaxX + padding, Random.Range(bounds.MinY, bounds.MaxY))
            };
        }
    }
}