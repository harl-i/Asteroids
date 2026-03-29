using Game.Core.Physics;

namespace Game.Core.Enemy
{
    public class UfoModel
    {
        public Physics2DEntity Entity { get; }

        public bool IsAlive => Entity.IsActive;

        public UfoModel(Physics2DEntity entity)
        {
            Entity = entity;
        }

        public void Destroy()
        {
            Entity.IsActive = false;
        }
    }
}