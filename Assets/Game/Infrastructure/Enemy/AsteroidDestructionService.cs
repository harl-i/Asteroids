using System;
using Game.Core.Enemy;
using Game.Core.Physics;
using Game.Core.Signals;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Enemy
{
    public class AsteroidDestructionService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private AsteroidFactory _asteroidFactory;
        private ConfigService _configService;

        public AsteroidDestructionService(
            SignalBus signalBus, 
            AsteroidFactory asteroidFactory,
            ConfigService configService)
        {
            _signalBus = signalBus;
            _asteroidFactory = asteroidFactory;
            _configService = configService;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CollisionSignal>(OnCollision);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CollisionSignal>(OnCollision);
        }

        private void OnCollision(CollisionSignal collisionSignal)
        {
            TryHandle(collisionSignal.A, collisionSignal.B);
            TryHandle(collisionSignal.B, collisionSignal.A);
        }

        private void TryHandle(Physics2DEntity bulletEntity, Physics2DEntity otherEntity)
        {
            if (bulletEntity.CollisionLayer != CollisionLayer.Bullet)
                return;

            if (otherEntity.PhysicsOwner is not AsteroidModel asteroid)
                return;

            asteroid.Destroy();

            _signalBus.Fire(new EnemyKilledSignal { type = asteroid.EnemyType });

            SpawnFragments(asteroid);
        }

        private void SpawnFragments(AsteroidModel asteroid)
        {
            if (asteroid.Size == AsteroidSize.Small)
                return;

            AsteroidSize newSize = asteroid.Size == AsteroidSize.Large
                ? AsteroidSize.Medium
                : AsteroidSize.Small;

            Vector2 pos = asteroid.Entity.Position;

            AsteroidModel a1 = _asteroidFactory.Create(pos, newSize);
            AsteroidModel a2 = _asteroidFactory.Create(pos, newSize);

            float fragmentSpeed = _configService.EnemyConfig.asteroidFragmentSpeed;

            a1.Entity.Velocity = UnityEngine.Random.insideUnitCircle * fragmentSpeed;
            a2.Entity.Velocity = UnityEngine.Random.insideUnitCircle * fragmentSpeed;
        }
    }
}