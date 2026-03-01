namespace Game.Core.Physics
{
    public interface ICollisionEvents
    {
        void OnCollision(Physics2DEntity a, Physics2DEntity b);
    }
}