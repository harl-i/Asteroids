using System;
using System.Collections.Generic;
using Game.Core.Enemy;
using Game.Core.Services;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Score
{
    public class ScoreTracker : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private Dictionary<EnemyType, int> _rewardByEnemyType;
        private IAnalyticsTracker _analyticsTracker;

        public int CurrentScore { get; private set; }

        public ScoreTracker(SignalBus signalBus, IAnalyticsTracker analyticsTracker)
        {
            _signalBus = signalBus;
            _analyticsTracker = analyticsTracker;

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
            _signalBus.Subscribe<RestartGameSignal>(OnRestart);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyKilledSignal>(OnEnemyKilled);
            _signalBus.Unsubscribe<RestartGameSignal>(OnRestart);
        }

        private void OnEnemyKilled(EnemyKilledSignal signal)
        {
            if (!_rewardByEnemyType.TryGetValue(signal.Type, out var reward))
                return;

            CurrentScore += reward;

            _signalBus.Fire(new ScoreChangedSignal 
            { 
                Score = CurrentScore 
            });

            _analyticsTracker.LogEvent(
                AnalyticsConstants.Events.EnemyKilled,
                AnalyticsConstants.Parameters.EnemyType,
                signal.Type.ToString());
        }

        private void OnRestart()
        {
            CurrentScore = 0;

            _signalBus.Fire(new ScoreChangedSignal
            {
                Score = CurrentScore
            });
        }
    }
}
