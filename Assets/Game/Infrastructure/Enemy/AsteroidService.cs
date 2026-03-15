using System.Collections.Generic;
using Game.Core.Enemy;
using Zenject;

namespace Game.Infrastructure.Enemy
{
    public class AsteroidService : ITickable
    {
        private List<AsteroidModel> _asteroids = new List<AsteroidModel>();

        public IReadOnlyList<AsteroidModel> Asteroids => _asteroids;
        public int ActiveCount => _asteroids.Count;

        public void Add(AsteroidModel asteroid)
        {
            if (asteroid == null)
                return;

            _asteroids.Add(asteroid);
        }

        public void Tick()
        {
            for (int i = _asteroids.Count - 1; i >= 0; i--)
            {
                if (!_asteroids[i].Entity.IsActive)
                {
                    _asteroids.RemoveAt(i);
                }
            }
        }
    }
}