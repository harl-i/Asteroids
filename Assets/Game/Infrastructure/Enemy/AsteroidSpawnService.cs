using Game.Core.Enemy;
using Game.Core.Game;
using Game.Core.World;
using Cysharp.Threading.Tasks;
using Game.Infrastructure.Game;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Enemy
{
    public class AsteroidSpawnService : IInitializable, ITickable
    {
        private AsteroidFactory _factory;
        private EnemyService _enemyService;
        private PhysicsWorldProvider _worldProvider;
        private ConfigService _config;
        private GameStateService _gameStateService;
        private SpawnPositionService _spawnPositionService;

        private float _spawnTimer;

        public AsteroidSpawnService(
            AsteroidFactory factory,
            EnemyService enemyService,
            PhysicsWorldProvider worldProvider,
            ConfigService config,
            GameStateService gameStateService,
            SpawnPositionService spawnPositionService)
        {
            _factory = factory;
            _enemyService = enemyService;
            _worldProvider = worldProvider;
            _config = config;
            _gameStateService = gameStateService;
            _spawnPositionService = spawnPositionService;
        }

        public async void Initialize()
        {
            await _config.LoadAsync();
            _spawnTimer = _config.EnemyConfig.AsteroidSpawnInterval;
        }

        public void Tick()
        {
            if (!_config.IsLoaded)
                return;

            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer > 0f)
                return;

            _spawnTimer = _config.EnemyConfig.AsteroidSpawnInterval;

            if (_enemyService.ActiveCount >= _config.WorldConfig.maxEnemies)
                return;

            SpawnLargeAsteroid();
        }

        private void SpawnLargeAsteroid()
        {
            WorldBounds bounds = _worldProvider.World.Bounds;
            Vector2 spawnPosition = _spawnPositionService.GetPositionOutside(bounds);
            AsteroidModel asteroid = _factory.Create(spawnPosition, AsteroidSize.Large);

            Vector2 targetDirection = (Vector2.zero - spawnPosition).normalized;
            Vector2 randomSpread = Random.insideUnitCircle * 0.35f;
            Vector2 direction = (targetDirection + randomSpread).normalized;

            float speed = Random.Range(
                _config.EnemyConfig.AsteroidMinSpeed,
                _config.EnemyConfig.AsteroidMaxSpeed);

            asteroid.Entity.Velocity = direction * speed;
        }
    }
}
