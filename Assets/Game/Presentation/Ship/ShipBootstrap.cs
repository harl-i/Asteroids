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

        private ShipRepository _shipRepository;
        private SignalBus _signalBus;
        private ConfigRepository _config;
        private PhysicsWorldProvider _worldProvider;

        [Inject]
        public void Construct(
            ShipRepository shipRepository,
            SignalBus signalBus,
            ConfigRepository config,
            PhysicsWorldProvider worldProvider)
        {
            _shipRepository = shipRepository;
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

            _shipRepository.CreateIfNeeded();
            _shipView.Bind(_shipRepository.Ship);
        }

        private void OnShipChanged()
        {
            _shipView.Bind(_shipRepository.Ship);
        }
    }
}
