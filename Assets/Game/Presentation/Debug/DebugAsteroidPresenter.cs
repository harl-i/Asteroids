using Game.Core.Enemy;
using Game.Infrastructure.Enemy;
using Game.Infrastructure.Physics;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Debug
{
    public class DebugAsteroidPresenter : MonoBehaviour
    {
        [SerializeField] private float _radius = 20f;
        [SerializeField] private float _mass = 3f;
        [SerializeField] private Vector2 _initialVelocity = new Vector2(-20f, 0f);

        private PhysicsWorldProvider _provider;

        private AsteroidFactory _asteroidFactory;
        private AsteroidModel _asteroid;

        [Inject]
        public void Construct(PhysicsWorldProvider provider, AsteroidFactory asteroidFactory)
        {
            _provider = provider;
            _asteroidFactory = asteroidFactory;
        }

        private void Start()
        {
            _asteroid = _asteroidFactory.Create(new Vector2(-0.03f, 14.03f), AsteroidSize.Large);
            _asteroid.Entity.Velocity = _initialVelocity;
        }

        private void Update()
        {
            if (_asteroid == null) return;

            if (!_asteroid.Entity.IsActive)
            {
                gameObject.SetActive(false);
                return;
            }

            transform.position = new Vector3(_asteroid.Entity.Position.x, _asteroid.Entity.Position.y, 0f);
        }
    }
}