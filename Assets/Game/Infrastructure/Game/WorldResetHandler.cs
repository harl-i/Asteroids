using System;
using Game.Core.Signals;
using Game.Infrastructure.Enemy;
using Game.Infrastructure.Weapons;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class WorldResetHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private BulletRegistry _bulletRegistry;
        private BulletShooter _bulletShooter;
        private EnemyRegistry _enemyRegistry;

        public WorldResetHandler(
            SignalBus signalBus,
            BulletRegistry bulletRegistry,
            BulletShooter bulletShooter,
            EnemyRegistry enemyRegistry)
        {
            _signalBus = signalBus;
            _bulletRegistry = bulletRegistry;
            _bulletShooter = bulletShooter;
            _enemyRegistry = enemyRegistry;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RestartGameSignal>(OnRestart);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<RestartGameSignal>(OnRestart);
        }

        private void OnRestart()
        {
            _bulletRegistry.Clear();
            _bulletShooter.ResetCooldown();
            _enemyRegistry.Clear();
        }
    }
}
