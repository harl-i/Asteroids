using Game.Infrastructure.Weapons;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Weapons
{
    public class BulletViewBootstrap : MonoBehaviour
    {
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private Transform _container;

        private BulletRegistry _bulletRegistry;
        private BulletViewFactory _bulletViewFactory;

        [Inject]
        private void Construct(
            BulletRegistry bulletRegistry,
            BulletViewFactory bulletViewFactory)
        {
            _bulletRegistry = bulletRegistry;
            _bulletViewFactory = bulletViewFactory;
        }

        private void Update()
        {
            foreach (var bullet in _bulletRegistry.Bullets)
            {
                _bulletViewFactory.GetOrCreate(bullet, _bulletPrefab, _container);
            }

            _bulletViewFactory.CleanupInactive(bullet => bullet.IsAlive);
        }
    }
}
