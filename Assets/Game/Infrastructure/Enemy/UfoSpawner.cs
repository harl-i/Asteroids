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
    public class UfoSpawner : IInitializable, ITickable
    {
        private UfoFactory _factory;
        private EnemyRegistry _enemyRegistry;
        private PhysicsWorldProvider _worldProvider;
        private GameStateMachine _gameStateMachine;
        private ConfigRepository _config;
        private SpawnPositionProvider _spawnPositionProvider;

        private float _spawnTimer;

        public UfoSpawner(
            UfoFactory factory,
            EnemyRegistry enemyRegistry,
            PhysicsWorldProvider worldProvider,
            GameStateMachine gameStateMachine,
            ConfigRepository config,
            SpawnPositionProvider spawnPositionProvider)
        {
            _factory = factory;
            _enemyRegistry = enemyRegistry;
            _worldProvider = worldProvider;
            _gameStateMachine = gameStateMachine;
            _config = config;
            _spawnPositionProvider = spawnPositionProvider;
        }

        public async void Initialize()
        {
            await _config.LoadAsync();
            _spawnTimer = _config.EnemyConfig.UfoSpawnInterval;
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

            _spawnTimer = _config.EnemyConfig.UfoSpawnInterval;

            if (_enemyRegistry.ActiveUfoCount > 0)
                return;

            SpawnUfo();
        }

        private void SpawnUfo()
        {
            WorldBounds bounds = _worldProvider.World.Bounds;
            Vector2 position = _spawnPositionProvider.GetPositionOutside(bounds);

            UfoModel ufo = _factory.Create(position);
            ufo.Entity.SetVelocity(Random.insideUnitCircle * _config.EnemyConfig.UfoSpawnSpeed);
        }
    }
}
