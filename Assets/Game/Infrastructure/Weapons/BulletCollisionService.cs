using System;
using Game.Core.Physics;
using Game.Core.Signals;
using Game.Core.Weapons;
using Zenject;

namespace Game.Infrastructure.Weapons
{
    public class BulletCollisionService : IInitializable, IDisposable
    {
        private SignalBus _signalBus;

        public BulletCollisionService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<CollisionSignal>(OnCollision);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CollisionSignal>(OnCollision);
        }

        private void OnCollision(CollisionSignal collisionSignal)
        {
            TryHandleBulletVsEnemy(collisionSignal.A, collisionSignal.B);
            TryHandleBulletVsEnemy(collisionSignal.B, collisionSignal.A);
        }

        private void TryHandleBulletVsEnemy(Physics2DEntity bulletEntity, Physics2DEntity otherEntity)
        {
            if (bulletEntity.CollisionLayer != CollisionLayer.Bullet)
                return;

            if (otherEntity.CollisionLayer != CollisionLayer.Enemy)
                return;

            if (!bulletEntity.IsActive || !otherEntity.IsActive)
                return;

            if (bulletEntity.PhysicsOwner is BulletModel bullet)
                bullet.Destroy();

            _signalBus.Fire(new EnemyDeathRequestedSignal
            {
                Entity = otherEntity
            });
        }
    }
}