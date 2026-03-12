using Game.Core.Physics;
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
        private Physics2DEntity _body;

        [Inject]
        public void Construct(PhysicsWorldProvider provider)
        {
            _provider = provider;
        }

        private void Start()
        {
            _body = _provider.World.CreateEntity(transform.position, _radius, _mass);
            _body.CollisionLayer = CollisionLayer.Enemy;
            _body.Restitution = 1f;
            _body.Velocity = _initialVelocity;
        }

        private void Update()
        {
            if (_body == null) return;

            if (!_body.IsActive)
            {
                gameObject.SetActive(false);
                return;
            }

            transform.position = new Vector3(_body.Position.x, _body.Position.y, 0f);
        }
    }
}