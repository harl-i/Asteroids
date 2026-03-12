using System.Collections.Generic;
using Game.Core.Weapons;

namespace Game.Infrastructure.Weapons
{
    public class BulletPool
    {
        private Stack<BulletModel> _pool = new Stack<BulletModel>();

        public bool HasAvailable => _pool.Count > 0;

        public BulletModel Get()
        {
            return _pool.Pop();
        }

        public void Return(BulletModel bullet)
        {
            _pool.Push(bullet);
        }
    }
}