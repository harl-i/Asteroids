using Game.Infrastructure.Ship;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Ship
{
    public class ShipBootstrap : MonoBehaviour
    {
        [SerializeField] private ShipView _shipView;

        private ShipControllerService _controller;

        [Inject]
        public void Construct(ShipControllerService controller)
        {
            _controller = controller;
        }

        private void Start()
        {
            _controller.CreateIfNeeded();
            _shipView.Bind(_controller.Ship);
        }
    }
}