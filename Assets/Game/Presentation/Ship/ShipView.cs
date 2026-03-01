using Game.Core.Ship;
using UnityEngine;

namespace Game.Presentation.Ship
{
    public class ShipView : MonoBehaviour
    {
        private ShipModel _model;
        private float _spriteCorrectAngle = 90f;

        public void Bind(ShipModel shipModel)
        {
            _model = shipModel;
        }

        private void Update()
        {
            if (_model == null) return;

            Vector2 position = _model.Entity.Position;
            transform.position = new Vector3(position.x, position.y, 0f);

            transform.rotation = Quaternion.Euler(0f, 0f, _model.RotationDeg - _spriteCorrectAngle);
        }
    }
}