using System;
using Game.Infrastructure.Enemy;
using Game.Infrastructure.Ship;
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
        private ShipControllerService _shipController;
        private LaserService _laserService;

        public WorldResetService(
            SignalBus signalBus,
            BulletService bulletService,
            EnemyService enemyService,
            UfoService ufoService,
            ShipControllerService shipController,
            LaserService laserService)
        {
            _signalBus = signalBus;
            _bulletService = bulletService;
            _enemyService = enemyService;
            _ufoService = ufoService;
            _shipController = shipController;
            _laserService = laserService;
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
            _laserService.ResetState();
            _shipController.ResetShip();
        }
    }
}