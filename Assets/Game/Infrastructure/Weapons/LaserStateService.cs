using Cysharp.Threading.Tasks;
using Game.Core.Game;
using Game.Core.Signals;
using Game.Infrastructure.Game;
using Game.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Weapons
{
    public class LaserStateService : ITickable, IInitializable
    {
        private ConfigService _config;
        private SignalBus _signalBus;
        private GameStateService _gameStateService;

        private int _currentCharges;
        private int _maxCharges;
        private float _cooldownSeconds;
        private float _cooldownRemaining;

        public int CurrentCharges => _currentCharges;
        public int MaxCharges => _maxCharges;
        public float CooldownRemaining => _cooldownRemaining;

        public LaserStateService(
            ConfigService config,
            SignalBus signalBus,
            GameStateService gameStateService)
        {
            _config = config;
            _signalBus = signalBus;
            _gameStateService = gameStateService;
        }

        public async void Initialize()
        {
            await _config.LoadAsync();
            ResetState();
        }

        public void Tick()
        {
            if (!_config.IsLoaded)
                return;

            if (_gameStateService.CurrentState != GameState.Playing)
                return;

            RechargeTick();
        }

        public bool TrySpendCharge()
        {
            if (_currentCharges <= 0)
                return false;

            _currentCharges--;

            if (_currentCharges < _maxCharges && _cooldownRemaining <= 0f)
            {
                _cooldownRemaining = _cooldownSeconds;
            }

            PublishState();
            return true;
        }

        public void ResetState()
        {
            _maxCharges = _config.PlayerConfig.LaserCharges;
            _currentCharges = _maxCharges;
            _cooldownSeconds = _config.PlayerConfig.LaserCooldown;
            _cooldownRemaining = 0f;

            PublishState();
        }

        private void RechargeTick()
        {
            if (_currentCharges >= _maxCharges)
                return;

            _cooldownRemaining -= Time.deltaTime;

            if (_cooldownRemaining <= 0f)
            {
                _currentCharges++;
                if (_currentCharges < _maxCharges)
                {
                    _cooldownRemaining = _cooldownSeconds;
                }
                else
                {
                    _cooldownRemaining = 0f;
                }

                PublishState();
            }
        }

        private void PublishState()
        {
            _signalBus.Fire(new LaserChargesChangedSignal
            {
                CurrentCharges = _currentCharges,
                MaxCharges = _maxCharges,
                CooldownRemaining = _cooldownRemaining
            });
        }
    }
}
