namespace Game.Core.Physics
{
    public interface ICollisionEvents
    {
       public void OnCollision(Physics2DEntity a, Physics2DEntity b);
    }
}