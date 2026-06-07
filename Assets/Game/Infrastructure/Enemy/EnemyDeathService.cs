using System;
using Game.Core.Enemy;
using Game.Core.Physics;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Enemies
{
    public class EnemyDeathService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private AsteroidSplitService _asteroidSplitService;

        public EnemyDeathService(
            SignalBus signalBus,
            AsteroidSplitService asteroidSplitService)
        {
            _signalBus = signalBus;
            _asteroidSplitService = asteroidSplitService;
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

            entity.SetActive(false);
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

            _asteroidSplitService.Split(asteroid);
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
