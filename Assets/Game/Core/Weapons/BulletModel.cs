using Game.Core.Physics;

namespace Game.Core.Weapons
{
    public class BulletModel
    {
        public Physics2DEntity Entity { get; }
        public float LifetimeRemaining { get; private set; }
        public bool IsAlive => Entity.IsActive && LifetimeRemaining > 0f;

        public BulletModel(Physics2DEntity physics2DEntity, float lifetimeSeconds)
        {
            Entity = physics2DEntity;
            LifetimeRemaining = lifetimeSeconds;
        }

        public void Tick(float dt)
        {
            if (!Entity.IsActive) return;

            LifetimeRemaining -= dt;

            if (LifetimeRemaining <= 0f)
            {
                Entity.IsActive = false;
            }
        }

        public void Destroy()
        {
            Entity.IsActive = false;
            LifetimeRemaining = 0f;
        }
    }
}