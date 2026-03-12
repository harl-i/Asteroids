using Game.Core.Physics;
using Game.Core.Weapons;
using Game.Infrastructure.Physics;
using UnityEngine;

namespace Game.Infrastructure.Weapons
{
    public class BulletFactory
    {
        private PhysicsWorldProvider _worldProvider;
        private ConfigService _config;

        public BulletFactory(PhysicsWorldProvider worldProvider, ConfigService configService)
        {
            _worldProvider = worldProvider;
            _config = configService;
        }

        public BulletModel Create(Vector2 position, Vector2 direction, Vector2 inheritedVelocity)
        {
            Physics2DEntity entity = _worldProvider.World.CreateEntity(position, 4f, 0.2f);
            entity.CollisionLayer = CollisionLayer.Bullet;
            entity.Restitution = 0f;
            entity.IsTrigger = true;

            float speed = _config.PlayerConfig.bulletSpeed;
            entity.Velocity = inheritedVelocity + direction.normalized * speed;

            var bullet = new BulletModel(entity, lifetimeSeconds: 2f);
            entity.PhysicsOwner = bullet;

            return bullet;
        }
    }
}