using Game.Core.Enemy;
using Game.Infrastructure.Enemy;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Enemy
{
    public class AsteroidViewBootstrap : MonoBehaviour
    {
        [SerializeField] private AsteroidView _asteroidPrefab;
        [SerializeField] private Transform _container;

        private EnemyRegistry _enemyRegistry;
        private AsteroidViewFactory _factory;

        [Inject]
        public void Construct(
            EnemyRegistry enemyRegistry,
            AsteroidViewFactory factory)
        {
            _enemyRegistry = enemyRegistry;
            _factory = factory;
        }

        private void Update()
        {
            foreach (var enemy in _enemyRegistry.Enemies)
            {
                if (enemy is AsteroidModel asteroid)
                {
                    _factory.GetOrCreate(asteroid, _asteroidPrefab, _container);
                }
            }

            _factory.CleanupInactive(asteroid => asteroid.IsAlive);
        }
    }
}
