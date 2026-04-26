using Game.Core.Signals;
using Game.Infrastructure.Ship;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Ship
{
    public class ShipBootstrap : MonoBehaviour
    {
        [SerializeField] private ShipView _shipView;

        private ShipControllerService _controller;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(ShipControllerService controller, SignalBus signalBus)
        {
            _controller = controller;
            _signalBus = signalBus;
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
            _controller.CreateIfNeeded();
            _shipView.Bind(_controller.Ship);
        }

        private void OnShipChanged()
        {
            _shipView.Bind(_controller.Ship);
        }
    }
}