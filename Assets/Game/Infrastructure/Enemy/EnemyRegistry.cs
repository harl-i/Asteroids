using System.Collections.Generic;
using Game.Core.Enemy;
using Zenject;

namespace Game.Infrastructure.Enemy
{
    public class EnemyRegistry : ITickable
    {
        private List<IEnemy> _enemies = new List<IEnemy>();

        public IReadOnlyList<IEnemy> Enemies => _enemies;
        public int ActiveCount => _enemies.Count;
        public int ActiveUfoCount => Count<UfoModel>();

        public void Add(IEnemy enemy)
        {
            if (enemy == null)
                return;

            _enemies.Add(enemy);
        }

        public void Tick()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (!_enemies[i].IsAlive)
                {
                    _enemies.RemoveAt(i);
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].Destroy();
            }

            _enemies.Clear();
        }

        public IEnumerable<TEnemy> GetEnemies<TEnemy>() where TEnemy : class, IEnemy
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] is TEnemy enemy)
                    yield return enemy;
            }
        }

        private int Count<TEnemy>() where TEnemy : class, IEnemy
        {
            int count = 0;

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] is TEnemy && _enemies[i].IsAlive)
                    count++;
            }

            return count;
        }
    }
}
