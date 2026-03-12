using Game.Core.Weapons;
using UnityEngine;

namespace Game.Presentation.Weapons
{
    public class BulletView : MonoBehaviour
    {
        private BulletModel _bulletModel;

        public void Bind(BulletModel bulletModel)
        {
            _bulletModel = bulletModel;
        }

        private void Update()
        {
            if (_bulletModel == null)
                return;

            gameObject.SetActive(_bulletModel.Entity.IsActive);

            if (!_bulletModel.Entity.IsActive)
                return;

            Vector2 position = _bulletModel.Entity.Position;
            transform.position = new Vector3(position.x, position.y, 0f);
        }
    }
}