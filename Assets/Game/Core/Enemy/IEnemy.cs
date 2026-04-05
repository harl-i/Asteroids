using Game.Core.Physics;

namespace Game.Core.Enemy
{
    public interface IEnemy
    {
        Physics2DEntity Entity { get; }
        EnemyType EnemyType { get; }
        bool IsAlive { get; }

        public void Destroy();
    }
}