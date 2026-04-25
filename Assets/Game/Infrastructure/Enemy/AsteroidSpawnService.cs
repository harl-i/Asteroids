using Game.Core.Enemy;
using Game.Core.Game;
using Game.Core.World;
using Game.Infrastructure.Game;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Enemy
{
    public class AsteroidSpawnService : ITickable
    {
        private AsteroidFactory _factory;
        private EnemyService _enemyService;
        private PhysicsWorldProvider _worldProvider;
        private ConfigService _config;
        private GameStateService _gameStateService;

        private float _spawnTimer;

        public AsteroidSpawnService(
            AsteroidFactory factory,
            EnemyService enemyService,
            PhysicsWorldProvider worldProvider,
            ConfigService config,
            GameStateService gameStateService)
        {
            _factory = factory;
            _enemyService = enemyService;
            _worldProvider = worldProvider;
            _config = config;
            _gameStateService = gameStateService;
        }

        public void Tick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            UnityEngine.Debug.Log("ASTEROIDS SPAWN WORKED!!!");
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer > 0f)
                return;

            _spawnTimer = _config.EnemyConfig.asteroidSpawnInterval;

            if (_enemyService.ActiveCount >= _config.WorldConfig.maxEnemies)
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