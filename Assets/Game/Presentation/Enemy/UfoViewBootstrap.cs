using Game.Infrastructure.Enemy;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Enemy
{
    public class UfoViewBootstrap : MonoBehaviour
    {
        [SerializeField] private UfoView _ufoPrefab;
        [SerializeField] private Transform _container;

        private UfoService _ufoService;
        private UfoViewFactory _factory;

        [Inject]
        public void Construct(UfoService ufoService)
        {
            _ufoService = ufoService;
        }

        private void Awake()
        {
            _factory = new UfoViewFactory(_ufoPrefab, _container);
        }

        private void Update()
        {
            foreach (var ufo in _ufoService.Ufos)
            {
                _factory.GetOrCreate(ufo);
            }
        }
    }
}