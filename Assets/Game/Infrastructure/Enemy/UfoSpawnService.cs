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

        private float _spawnTimer;

        public UfoSpawnService(
            UfoFactory factory,
            UfoService ufoService,
            PhysicsWorldProvider worldProvider,
            GameStateService gameStateService,
            ConfigService config)
        {
            _factory = factory;
            _ufoService = ufoService;
            _worldProvider = worldProvider;
            _gameStateService = gameStateService;
            _config = config;
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
            Vector2 position = GetSpawnPositionOutside(bounds);

            UfoModel ufo = _factory.Create(position);
            ufo.Entity.Velocity = Random.insideUnitCircle * _config.EnemyConfig.UfoSpawnSpeed;
        }

        private Vector2 GetSpawnPositionOutside(WorldBounds bounds)
        {
            float padding = _config.EnemyConfig.SpawnPadding;
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
