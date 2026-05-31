using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Signals;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Weapons
{
    public class LaserView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _showDuration = 0.08f;

        private SignalBus _signalBus;
        private CancellationTokenSource _showLaserCancellation;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _lineRenderer.enabled = false;
        }

        private void OnEnable()
        {
            _showLaserCancellation = new CancellationTokenSource();
            _signalBus.Subscribe<LaserFiredSignal>(OnLaserFired);
        }

        private void OnDisable()
        {
            if (_signalBus != null)
                _signalBus.Unsubscribe<LaserFiredSignal>(OnLaserFired);

            if (_showLaserCancellation != null)
            {
                _showLaserCancellation.Cancel();
                _showLaserCancellation.Dispose();
                _showLaserCancellation = null;
            }

            _lineRenderer.enabled = false;
        }

        private void OnLaserFired(LaserFiredSignal signal)
        {
            ShowLaserAsync(signal.Start, signal.End, _showLaserCancellation.Token).Forget();
        }

        private async UniTask ShowLaserAsync(Vector2 start, Vector2 end, CancellationToken cancellationToken)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, new Vector3(start.x, start.y, 0f));
            _lineRenderer.SetPosition(1, new Vector3(end.x, end.y, 0f));

            try
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(_showDuration), cancellationToken: cancellationToken);
            }
            catch (System.OperationCanceledException)
            {
                return;
            }

            _lineRenderer.enabled = false;
        }
    }
}
