using System;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Debug
{
    public class ShipDebugListener : IInitializable, IDisposable
    {
        private SignalBus _signalBus;

        public ShipDebugListener(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ShipDamagedSignal>(OnShipDamaged);
            _signalBus.Subscribe<ShipInvulnerabilityStartedSignal>(OnInvulnerabilityStarted);
            _signalBus.Subscribe<ShipInvulnerabilityEndedSignal>(OnInvulnerabilityEnded);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ShipDamagedSignal>(OnShipDamaged);
            _signalBus.Unsubscribe<ShipInvulnerabilityStartedSignal>(OnInvulnerabilityStarted);
            _signalBus.Unsubscribe<ShipInvulnerabilityEndedSignal>(OnInvulnerabilityEnded);
            _signalBus.Unsubscribe<GameOverSignal>(OnGameOver);
        }

        private void OnShipDamaged(ShipDamagedSignal signal)
        {
            UnityEngine.Debug.Log($"Ship damaged. HP: {signal.CurrentHealth}");
        }

        private void OnInvulnerabilityStarted()
        {
            UnityEngine.Debug.Log("Invulnerability started");
        }

        private void OnInvulnerabilityEnded()
        {
            UnityEngine.Debug.Log("Invulnerability ended");
        }

        private void OnGameOver()
        {
            UnityEngine.Debug.Log("Game Over");
        }
    }
}