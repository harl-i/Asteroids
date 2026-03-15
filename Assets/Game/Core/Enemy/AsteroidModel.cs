using Game.Core.Physics;

namespace Game.Core.Enemy
{
    public class AsteroidModel
    {
        public Physics2DEntity Entity { get; }
        public AsteroidSize Size { get; }

        public bool IsAlive => Entity.IsActive;

        public AsteroidModel(Physics2DEntity physics2DEntity, AsteroidSize asteroidSize)
        {
            Entity = physics2DEntity;
            Size = asteroidSize;
        }

        public void Destroy()
        {
            Entity.IsActive = false;
        }
    }
}