using Game.Infrastructure.Weapons;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Debug
{
    public class BulletDebugDrawer : MonoBehaviour
    {
        private BulletRegistry _bulletRegistry;

        [Inject]
        public void Construct(BulletRegistry bulletRegistry)
        {
            _bulletRegistry = bulletRegistry;
        }

        private void OnDrawGizmos()
        {
            if (_bulletRegistry == null)
                return;

            foreach (var bullet in _bulletRegistry.Bullets)
            {
                if (!bullet.IsAlive)
                    continue;

                Vector2 position = bullet.Entity.Position;
                Gizmos.DrawWireSphere(new Vector3(position.x, position.y, 0f), bullet.Entity.Radius);
            }
        }
    }
}
