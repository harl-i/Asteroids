using Game.Core.Enemy;
using Game.Core.Physics;
using Game.Infrastructure.Physics;
using UnityEngine;

namespace Game.Infrastructure.Enemy
{
    public class UfoFactory
    {
        private PhysicsWorldProvider _world;
        private EnemyService _enemyService;

        private float _radius = 1.4f;
        private float _mass = 1.5f;

        public UfoFactory(PhysicsWorldProvider world, EnemyService enemyService)
        {
            _world = world;
            _enemyService = enemyService;
        }

        public UfoModel Create(Vector2 position)
        {
            Physics2DEntity entity = _world.World.CreateEntity(position, _radius, _mass);
            entity.CollisionLayer = CollisionLayer.Enemy;
            entity.Restitution = 1f;

            UfoModel ufo = new UfoModel(entity);
            entity.PhysicsOwner = ufo;

            _enemyService.Add(ufo);
            return ufo;
        }
    }
}