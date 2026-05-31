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
    public class UfoSpawnService : IInitializable, ITickable
    {
        private UfoFactory _factory;
        private UfoService _ufoService;
        private PhysicsWorldProvider _worldProvider;
        private GameStateService _gameStateService;
        private ConfigService _config;
        private SpawnPositionService _spawnPositionService;

        private float _spawnTimer;

        public UfoSpawnService(
            UfoFactory factory,
            UfoService ufoService,
            PhysicsWorldProvider worldProvider,
            GameStateService gameStateService,
            ConfigService config,
            SpawnPositionService spawnPositionService)
        {
            _factory = factory;
            _ufoService = ufoService;
            _worldProvider = worldProvider;
            _gameStateService = gameStateService;
            _config = config;
            _spawnPositionService = spawnPositionService;
        }

        public void Initialize()
        {
            _spawnTimer = _config.EnemyConfig.UfoSpawnInterval;
        }

        public void Tick()
        {
            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer > 0f)
                return;

            _spawnTimer = _config.EnemyConfig.UfoSpawnInterval;

            if (_ufoService.ActiveCount > 0)
                return;

            SpawnUfo();
        }

        private void SpawnUfo()
        {
            WorldBounds bounds = _worldProvider.World.Bounds;
            Vector2 position = _spawnPositionService.GetPositionOutside(bounds);

            UfoModel ufo = _factory.Create(position);
            ufo.Entity.Velocity = Random.insideUnitCircle * _config.EnemyConfig.UfoSpawnSpeed;
        }
    }
}
