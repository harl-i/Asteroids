using Game.Core.Enemy;
using Game.Core.Game;
using Game.Core.Ship;
using Game.Core.World;
using Game.Infrastructure.Game;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Ship;
using Game.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Enemy
{
    public class UfoMovementController : ITickable
    {
        private EnemyRegistry _enemyRegistry;
        private ShipRepository _shipRepository;
        private GameStateMachine _gameStateMachine;
        private PhysicsWorldProvider _worldProvider;
        private ConfigRepository _config;

        public UfoMovementController(
            EnemyRegistry enemyRegistry,
            ShipRepository shipRepository,
            GameStateMachine gameStateMachine,
            PhysicsWorldProvider worldProvider,
            ConfigRepository config)
        {
            _enemyRegistry = enemyRegistry;
            _shipRepository = shipRepository;
            _gameStateMachine = gameStateMachine;
            _worldProvider = worldProvider;
            _config = config;
        }

        public void Tick()
        {
            if (!_config.IsLoaded)
                return;

            if (_gameStateMachine.CurrentState != GameState.Playing)
                return;

            ShipModel ship = _shipRepository.Ship;
            if (ship == null)
                return;

            foreach (var ufo in _enemyRegistry.GetEnemies<UfoModel>())
            {
                if (!ufo.Entity.IsActive)
                    continue;

                Vector2 toShip = GetShortestWrappedDelta(ufo.Entity.Position, ship.Entity.Position);
                if (toShip.sqrMagnitude < 0.001f)
                    continue;

                Vector2 direction = toShip.normalized;
                Vector2 desiredVelocity = direction * _config.EnemyConfig.UfoMaxSpeed;
                float maxDelta = _config.EnemyConfig.UfoAcceleration * Time.deltaTime;

                ufo.Entity.SetVelocity(Vector2.MoveTowards(ufo.Entity.Velocity, desiredVelocity, maxDelta));
            }
        }

        private Vector2 GetShortestWrappedDelta(Vector2 from, Vector2 to)
        {
            WorldBounds bounds = _worldProvider.World.Bounds;
            Vector2 delta = to - from;
            float halfWidth = bounds.Width * 0.5f;
            float halfHeight = bounds.Height * 0.5f;

            if (delta.x > halfWidth) delta.x -= bounds.Width;
            else if (delta.x < -halfWidth) delta.x += bounds.Width;

            if (delta.y > halfHeight) delta.y -= bounds.Height;
            else if (delta.y < -halfHeight) delta.y += bounds.Height;

            return delta;
        }
    }
}
