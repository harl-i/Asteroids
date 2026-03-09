using System;
using Cysharp.Threading.Tasks;
using Game.Core.Physics;
using Game.Core.Ship;
using Zenject;

namespace Game.Infrastructure.Ship
{
    public class ShipDamageService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private ShipControllerService _shipController;

        private int _damagePerHit = 1;
        private float _invulnerabilityDuration = 3f;
        private int _timeMultiplier = 1000;

        public ShipDamageService(SignalBus signalBus, ShipControllerService shipController)
        {
            _signalBus = signalBus;
            _shipController = shipController;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CollisionSignal>(OnCollision);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CollisionSignal>(OnCollision);
        }

        private void OnCollision(CollisionSignal signal)
        {
            ShipModel ship = _shipController.Ship;
            if (ship == null) return;

            bool isShipEnemyCollision =
                (signal.A.CollisionLayer == CollisionLayer.Ship && signal.B.CollisionLayer == CollisionLayer.Enemy) ||
                (signal.A.CollisionLayer == CollisionLayer.Enemy && signal.B.CollisionLayer == CollisionLayer.Ship);

            if (!isShipEnemyCollision || !ship.CanTakeDamage()) return;

            ship.ApplyDamage(_damagePerHit);
            _signalBus.Fire(new ShipDamagedSignal { CurrentHealth = ship.CurrentHealth });

            if (ship.CurrentHealth <= 0)
            {
                _signalBus.Fire<GameOverSignal>();
                return;
            }

            StartInvulnerability(ship).Forget();
        }

        private async UniTaskVoid StartInvulnerability(ShipModel ship)
        {
            ship.SetInvulnerable(true);
            ship.SetControlLocked(true);

            _signalBus.Fire<ShipInvulnerabilityStartedSignal>();

            await UniTask.Delay((int)(_invulnerabilityDuration * _timeMultiplier));

            if (ship.CurrentHealth <= 0) return;

            ship.SetInvulnerable(false);
            ship.SetControlLocked(false);

            _signalBus.Fire<ShipInvulnerabilityEndedSignal>();
        }
    }
}