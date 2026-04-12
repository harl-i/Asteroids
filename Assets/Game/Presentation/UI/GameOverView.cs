using Game.Core.Game;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Presentation.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _label;

        private SignalBus _signalBus;
        private string _gameOverLabel = "GAME OVER\nPress R to Restart";

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _gameOverPanel.SetActive(false);

            if (_label != null)
            {
                _label.text = _gameOverLabel;
            }
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnDisable()
        {
            if (_signalBus == null)
                return;

            _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            _gameOverPanel.SetActive(signal.State == GameState.GameOver);
        }
    }
}