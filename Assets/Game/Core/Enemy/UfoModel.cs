using Game.Core.Physics;

namespace Game.Core.Enemy
{
    public class UfoModel : IEnemy
    {
        public Physics2DEntity Entity { get; }

        public EnemyType EnemyType => EnemyType.UFO;
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