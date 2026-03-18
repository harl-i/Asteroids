using System;
using System.Collections.Generic;
using Game.Core.Enemy;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Score
{
    public class ScoreService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private Dictionary<EnemyType, int> _rewardByEnemyType;

        public int CurrentScore { get; private set; }

        public ScoreService(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _rewardByEnemyType = new Dictionary<EnemyType, int>
            {
                { EnemyType.AsteroidLarge, 20 },
                { EnemyType.AsteroidMedium, 50 },
                { EnemyType.AsteroidSmall, 100 },
                { EnemyType.UFO, 200 },
            };
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EnemyKilledSignal>(OnEnemyKilled);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyKilledSignal>(OnEnemyKilled);
        }

        private void OnEnemyKilled(EnemyKilledSignal signal)
        {
            if (!_rewardByEnemyType.TryGetValue(signal.Type, out var reward))
                return;

            CurrentScore += reward;

            _signalBus.Fire(new ScoreChangedSignal { Score = CurrentScore });
        }
    }
}