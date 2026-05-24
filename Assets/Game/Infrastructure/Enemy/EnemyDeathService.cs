using System;
using Game.Core.Enemy;
using Game.Core.Physics;
using Game.Core.Signals;
using Game.Infrastructure.Enemy;
using Game.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Enemies
{
    public class EnemyDeathService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private AsteroidFactory _asteroidFactory;
        private ConfigService _configService;

        public EnemyDeathService(
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
            _signalBus.Subscribe<EnemyDeathRequestedSignal>(OnEnemyDeathRequested);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyDeathRequestedSignal>(OnEnemyDeathRequested);
        }

        private void OnEnemyDeathRequested(EnemyDeathRequestedSignal signal)
        {
            Physics2DEntity entity = signal.Entity;

            if (entity == null || !entity.IsActive)
                return;

            if (entity.PhysicsOwner is AsteroidModel asteroid)
            {
                KillAsteroid(asteroid);
                return;
            }

            if (entity.PhysicsOwner is UfoModel ufo)
            {
                KillUfo(ufo);
                return;
            }

            entity.IsActive = false;
        }

        private void KillAsteroid(AsteroidModel asteroid)
        {
            if (!asteroid.Entity.IsActive)
                return;

            asteroid.Destroy();

            _signalBus.Fire(new EnemyKilledSignal
            {
                Type = asteroid.EnemyType
            });

            SpawnFragments(asteroid);
        }

        private void SpawnFragments(AsteroidModel asteroid)
        {
            if (asteroid.Size == AsteroidSize.Small)
                return;

            AsteroidSize newSize = asteroid.Size == AsteroidSize.Large
                ? AsteroidSize.Medium
                : AsteroidSize.Small;

            Vector2 position = asteroid.Entity.Position;

            AsteroidModel a1 = _asteroidFactory.Create(position, newSize);
            AsteroidModel a2 = _asteroidFactory.Create(position, newSize);

            float fragmentSpeed = _configService.EnemyConfig.AsteroidFragmentSpeed;
            a1.Entity.Velocity = UnityEngine.Random.insideUnitCircle * fragmentSpeed;
            a2.Entity.Velocity = UnityEngine.Random.insideUnitCircle * fragmentSpeed;
        }

        private void KillUfo(UfoModel ufo)
        {
            if (!ufo.Entity.IsActive)
                return;

            ufo.Destroy();

            _signalBus.Fire(new EnemyKilledSignal
            {
                Type = EnemyType.UFO
            });
        }
    }
}
