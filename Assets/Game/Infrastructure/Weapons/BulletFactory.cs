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

        private float _lifetimeSeconds = 0.4f;
        private float _radius = 0.3f;
        private float _mass = 0.2f;

        public BulletFactory(PhysicsWorldProvider worldProvider, ConfigService configService)
        {
            _worldProvider = worldProvider;
            _config = configService;
        }

        public BulletModel Create()
        {
            Physics2DEntity entity = _worldProvider.World.CreateEntity(Vector2.zero, _radius, _mass);
            entity.CollisionLayer = CollisionLayer.Bullet;
            entity.Restitution = 0f;
            entity.IsTrigger = true;
            entity.IsActive = false;

            var bullet = new BulletModel(entity, 0f);
            entity.PhysicsOwner = bullet;

            return bullet;
        }

        public void Activate(BulletModel bullet, Vector2 position, Vector2 direction, Vector2 inheritedVelocity)
        {
            float speed = _config.PlayerConfig.bulletSpeed;
            Vector2 velocity = inheritedVelocity + direction.normalized * speed;

            bullet.Activate(position, velocity, _lifetimeSeconds);
        }
    }
}