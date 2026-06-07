using System;
using Game.Core.Signals;
using Game.Infrastructure.Enemy;
using Game.Infrastructure.Weapons;
using Zenject;

namespace Game.Infrastructure.Game
{
    public class WorldResetService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private BulletService _bulletService;
        private BulletShooterService _bulletShooterService;
        private EnemyService _enemyService;

        public WorldResetService(
            SignalBus signalBus,
            BulletService bulletService,
            BulletShooterService bulletShooterService,
            EnemyService enemyService)
        {
            _signalBus = signalBus;
            _bulletService = bulletService;
            _bulletShooterService = bulletShooterService;
            _enemyService = enemyService;
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
            _bulletService.Clear();
            _bulletShooterService.ResetCooldown();
            _enemyService.Clear();
        }
    }
}
