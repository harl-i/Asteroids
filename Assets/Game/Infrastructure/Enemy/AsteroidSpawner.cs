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
    public class AsteroidSpawner : IInitializable, ITickable
    {
        private AsteroidFactory _factory;
        private EnemyRegistry _enemyRegistry;
        private PhysicsWorldProvider _worldProvider;
        private ConfigRepository _config;
        private GameStateMachine _gameStateMachine;
        private SpawnPositionProvider _spawnPositionProvider;

        private float _spawnTimer;

        public AsteroidSpawner(
            AsteroidFactory factory,
            EnemyRegistry enemyRegistry,
            PhysicsWorldProvider worldProvider,
            ConfigRepository config,
            GameStateMachine gameStateMachine,
            SpawnPositionProvider spawnPositionProvider)
        {
            _factory = factory;
            _enemyRegistry = enemyRegistry;
            _worldProvider = worldProvider;
            _config = config;
            _gameStateMachine = gameStateMachine;
            _spawnPositionProvider = spawnPositionProvider;
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

            if (_gameStateMachine.CurrentState != GameState.Playing)
                return;

            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer > 0f)
                return;

            _spawnTimer = _config.EnemyConfig.AsteroidSpawnInterval;

            if (_enemyRegistry.ActiveCount >= _config.WorldConfig.maxEnemies)
                return;

            SpawnLargeAsteroid();
        }

        private void SpawnLargeAsteroid()
        {
            WorldBounds bounds = _worldProvider.World.Bounds;
            Vector2 spawnPosition = _spawnPositionProvider.GetPositionOutside(bounds);
            AsteroidModel asteroid = _factory.Create(spawnPosition, AsteroidSize.Large);

            Vector2 targetDirection = (Vector2.zero - spawnPosition).normalized;
            Vector2 randomSpread = Random.insideUnitCircle * 0.35f;
            Vector2 direction = (targetDirection + randomSpread).normalized;

            float speed = Random.Range(
                _config.EnemyConfig.AsteroidMinSpeed,
                _config.EnemyConfig.AsteroidMaxSpeed);

            asteroid.Entity.SetVelocity(direction * speed);
        }
    }
}
