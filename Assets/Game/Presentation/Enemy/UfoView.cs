using Game.Core.Enemy;
using Game.Presentation.Common;
using UnityEngine;

namespace Game.Presentation.Enemy
{
    public class UfoView : MonoBehaviour, IBindableView<UfoModel>
    {
        private UfoModel _model;

        public void Bind(UfoModel model)
        {
            _model = model;
        }

        private void Update()
        {
            if (_model == null)
                return;

            if (!_model.Entity.IsActive)
            {
                gameObject.SetActive(false);
                return;
            }

            Vector2 position = _model.Entity.Position;
            transform.position = new Vector3(position.x, position.y, 0f);
        }
    }
}
