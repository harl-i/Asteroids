using Game.Core.Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Presentation.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            UpdateScore(0);
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreChangedSignal signal)
        {
            UpdateScore(signal.Score);
        }

        private void UpdateScore(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
    }
}