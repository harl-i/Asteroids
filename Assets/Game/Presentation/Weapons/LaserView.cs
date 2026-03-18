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
            _signalBus.Subscribe<LaserFiredSignal>(OnLaserFired);
        }

        private void OnDisable()
        {
            if (_signalBus == null) return;
            _signalBus.Unsubscribe<LaserFiredSignal>(OnLaserFired);
        }

        private void OnLaserFired(LaserFiredSignal signal)
        {
            ShowLaser(signal.Start, signal.End).Forget();
        }

        private async UniTaskVoid ShowLaser(Vector2 start, Vector2 end)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, new Vector3(start.x, start.y, 0f));
            _lineRenderer.SetPosition(1, new Vector3(end.x, end.y, 0f));

            await UniTask.Delay((int)(_showDuration * 1000));

            _lineRenderer.enabled = false;
        }
    }
}