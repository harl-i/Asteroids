using Game.Core.Enemy;
using Game.Infrastructure.Enemy;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Enemy
{
    public class UfoViewBootstrap : MonoBehaviour
    {
        [SerializeField] private UfoView _ufoPrefab;
        [SerializeField] private Transform _container;

        private EnemyRegistry _enemyRegistry;
        private UfoViewFactory _factory;

        [Inject]
        public void Construct(
            EnemyRegistry enemyRegistry,
            UfoViewFactory factory)
        {
            _enemyRegistry = enemyRegistry;
            _factory = factory;
        }

        private void Update()
        {
            foreach (var enemy in _enemyRegistry.Enemies)
            {
                if (enemy is UfoModel ufo)
                {
                    _factory.GetOrCreate(ufo, _ufoPrefab, _container);
                }
            }

            _factory.CleanupInactive(ufo => ufo.IsAlive);
        }
    }
}
