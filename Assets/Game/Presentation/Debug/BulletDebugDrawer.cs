using Game.Infrastructure.Weapons;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Debug
{
    public class BulletDebugDrawer : MonoBehaviour
    {
        private BulletService _bulletService;

        [Inject]
        public void Construct(BulletService bulletService)
        {
            _bulletService = bulletService;
        }

        private void OnDrawGizmos()
        {
            if (_bulletService == null)
                return;

            foreach (var bullet in _bulletService.Bullets)
            {
                if (!bullet.IsAlive)
                    continue;

                Vector2 position = bullet.Entity.Position;
                Gizmos.DrawWireSphere(new Vector3(position.x, position.y, 0f), bullet.Entity.Radius);
            }
        }
    }
}