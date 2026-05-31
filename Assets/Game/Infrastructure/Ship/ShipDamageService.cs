using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Physics;
using Game.Core.Ship;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Ship
{
    public class ShipDamageService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private ShipControllerService _shipController;
        private CancellationTokenSource _lifetimeCancellation;

        private int _damagePerHit = 1;
        private float _invulnerabilityDuration = 3f;

        public ShipDamageService(SignalBus signalBus, ShipControllerService shipController)
        {
            _signalBus = signalBus;
            _shipController = shipController;
            _lifetimeCancellation = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CollisionSignal>(OnCollision);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CollisionSignal>(OnCollision);
            _lifetimeCancellation.Cancel();
            _lifetimeCancellation.Dispose();
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

            _signalBus.Fire(new ShipDamagedSignal 
            { 
                CurrentHealth = ship.CurrentHealth 
            });

            if (ship.CurrentHealth <= 0)
            {
                _signalBus.Fire<GameOverSignal>();
                return;
            }

            StartInvulnerabilityAsync(ship, _lifetimeCancellation.Token).Forget();
        }

        private async UniTask StartInvulnerabilityAsync(ShipModel ship, CancellationToken cancellationToken)
        {
            ship.SetInvulnerable(true);
            ship.SetControlLocked(true);

            _signalBus.Fire<ShipInvulnerabilityStartedSignal>();

            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_invulnerabilityDuration), cancellationToken: cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            if (cancellationToken.IsCancellationRequested || ship.CurrentHealth <= 0) return;

            ship.SetInvulnerable(false);
            ship.SetControlLocked(false);

            _signalBus.Fire<ShipInvulnerabilityEndedSignal>();
        }
    }
}
