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
        public void Construct(EnemyService enemyService)
        {
            _enemyService = enemyService;
        }

        private void Awake()
        {
            _factory = new AsteroidViewFactory(_asteroidPrefab, _container);
        }

        private void Update()
        {
            foreach (var enemy in _enemyService.Enemies)
            {
                if (enemy is AsteroidModel asteroid)
                {
                    _factory.GetOrCreate(asteroid);
                }
            }
        }
    }
}