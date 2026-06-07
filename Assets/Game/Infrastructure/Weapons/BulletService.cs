using System.Collections.Generic;
using Game.Core.Weapons;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Weapons
{
    public class BulletService : ITickable
    {
        private BulletPool _bulletPool;

        private List<BulletModel> _bullets = new List<BulletModel>();

        public IReadOnlyList<BulletModel> Bullets => _bullets;

        public BulletService(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public void Tick()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                BulletModel bullet = _bullets[i];
                bullet.Tick(Time.deltaTime);

                if (!bullet.IsAlive)
                {
                    DespawnBullet(bullet);
                    _bullets.RemoveAt(i);
                }
            }
        }

        public void Add(BulletModel bullet)
        {
            _bullets.Add(bullet);
        }

        public void Clear()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                var bullet = _bullets[i];
                bullet.Destroy();
                _bulletPool.Return(bullet);
            }

            _bullets.Clear();
        }

        private void DespawnBullet(BulletModel bullet)
        {
            bullet.Destroy();
            _bulletPool.Return(bullet);
        }
    }
}
