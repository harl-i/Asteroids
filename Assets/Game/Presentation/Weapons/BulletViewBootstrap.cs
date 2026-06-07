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
        private void Construct(
            BulletService bulletService,
            BulletViewFactory bulletViewFactory)
        {
            _bulletService = bulletService;
            _bulletViewFactory = bulletViewFactory;
        }

        private void Update()
        {
            foreach (var bullet in _bulletService.Bullets)
            {
                _bulletViewFactory.GetOrCreate(bullet, _bulletPrefab, _container);
            }

            _bulletViewFactory.CleanupInactive(bullet => bullet.IsAlive);
        }
    }
}
