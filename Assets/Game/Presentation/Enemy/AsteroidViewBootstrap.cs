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

        private EnemyService _enemyService;
        private AsteroidViewFactory _factory;

        [Inject]
        public void Construct(
            EnemyService enemyService,
            AsteroidViewFactory factory)
        {
            _enemyService = enemyService;
            _factory = factory;
        }

        private void Update()
        {
            foreach (var enemy in _enemyService.Enemies)
            {
                if (enemy is AsteroidModel asteroid)
                {
                    _factory.GetOrCreate(asteroid, _asteroidPrefab, _container);
                }
            }
        }
    }
}
