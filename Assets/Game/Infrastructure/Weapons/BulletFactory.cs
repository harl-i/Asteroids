using Game.Core.Physics;
using Game.Core.Weapons;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Services;
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

        public BulletModel Create()
        {
            float radius = _config.PlayerConfig.BulletRadius;
            float mass = _config.PlayerConfig.BulletMass;

            Physics2DEntity entity = _worldProvider.World.CreateEntity(Vector2.zero, radius, mass);
            entity.ConfigureCollision(CollisionLayer.Bullet, 0f, true);
            entity.SetActive(false);

            var bullet = new BulletModel(entity, 0f);
            entity.SetPhysicsOwner(bullet);

            return bullet;
        }

        public void Activate(BulletModel bullet, Vector2 position, Vector2 direction, Vector2 inheritedVelocity)
        {
            float speed = _config.PlayerConfig.BulletSpeed;
            float lifetimeSeconds = _config.PlayerConfig.BulletLifetimeSeconds;
            Vector2 velocity = inheritedVelocity + direction.normalized * speed;

            bullet.Activate(position, velocity, lifetimeSeconds);
        }
    }
}
