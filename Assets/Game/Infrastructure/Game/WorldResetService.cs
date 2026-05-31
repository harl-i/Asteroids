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
        private EnemyService _enemyService;
        private UfoService _ufoService;

        public WorldResetService(
            SignalBus signalBus,
            BulletService bulletService,
            EnemyService enemyService,
            UfoService ufoService)
        {
            _signalBus = signalBus;
            _bulletService = bulletService;
            _enemyService = enemyService;
            _ufoService = ufoService;
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
            _enemyService.Clear();
            _ufoService.Clear();
        }
    }
}
