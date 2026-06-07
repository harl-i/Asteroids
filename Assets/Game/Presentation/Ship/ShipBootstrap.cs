using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Signals;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Services;
using Game.Infrastructure.Ship;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Ship
{
    public class ShipBootstrap : MonoBehaviour
    {
        [SerializeField] private ShipView _shipView;

        private ShipService _shipService;
        private SignalBus _signalBus;
        private ConfigService _config;
        private PhysicsWorldProvider _worldProvider;

        [Inject]
        public void Construct(
            ShipService shipService,
            SignalBus signalBus,
            ConfigService config,
            PhysicsWorldProvider worldProvider)
        {
            _shipService = shipService;
            _signalBus = signalBus;
            _config = config;
            _worldProvider = worldProvider;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ShipChangedSignal>(OnShipChanged);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ShipChangedSignal>(OnShipChanged);
        }

        private void Start()
        {
            CreateShipAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTask CreateShipAsync(CancellationToken cancellationToken)
        {
            await _config.LoadAsync();
            await UniTask.WaitUntil(() => _worldProvider.World != null, cancellationToken: cancellationToken);

            _shipService.CreateIfNeeded();
            _shipView.Bind(_shipService.Ship);
        }

        private void OnShipChanged()
        {
            _shipView.Bind(_shipService.Ship);
        }
    }
}
