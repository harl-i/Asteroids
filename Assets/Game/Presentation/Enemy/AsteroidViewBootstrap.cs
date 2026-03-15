using Game.Infrastructure.Enemy;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Enemy
{
    public class AsteroidViewBootstrap : MonoBehaviour
    {
        [SerializeField] private AsteroidView _asteroidPrefab;
        [SerializeField] private Transform _container;

        private AsteroidService _asteroidService;
        private AsteroidViewFactory _factory;

        [Inject]
        public void Construct(AsteroidService asteroidService)
        {
            _asteroidService = asteroidService;
        }

        private void Awake()
        {
            _factory = new AsteroidViewFactory(_asteroidPrefab, _container);
        }

        private void Update()
        {
            foreach (var asteroid in _asteroidService.Asteroids)
            {
                _factory.GetOrCreate(asteroid);
            }
        }
    }
}