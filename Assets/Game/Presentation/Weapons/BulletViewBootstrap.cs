using Game.Infrastructure.Weapons;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Weapons
{
    public class BulletViewBootstrap : MonoBehaviour
    {
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private Transform _container;

        private BulletService _bulletService;
        private BulletViewFactory _bulletViewFactory;

        [Inject]
        private void Construct(BulletService bulletService)
        {
            _bulletService = bulletService;
        }

        private void Awake()
        {
            _bulletViewFactory = new BulletViewFactory(_bulletPrefab, _container);
        }

        private void Update()
        {
            foreach (var bullet in _bulletService.Bullets)
            {
                _bulletViewFactory.GetOrCreate(bullet);
            }
        }
    }
}