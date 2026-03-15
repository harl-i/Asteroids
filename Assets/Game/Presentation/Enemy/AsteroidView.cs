using Game.Core.Enemy;
using UnityEngine;

namespace Game.Presentation.Enemy
{
    public class AsteroidView : MonoBehaviour
    {
        private AsteroidModel _asteroidModel;

        private float _largeSize = 2.5f;
        private float _mediumSize = 1.5f;
        private float _smallSize = 1f;

        public void Bind(AsteroidModel asteroidModel)
        {
            _asteroidModel = asteroidModel;
            UpdateScale();
        }

        private void Update()
        {
            if (_asteroidModel == null)
                return;

            if (!_asteroidModel.Entity.IsActive)
            {
                gameObject.SetActive(false);
                return;
            }

            Vector2 position = _asteroidModel.Entity.Position;
            transform.position = new Vector3(position.x, position.y, 0f);
        }

        private void UpdateScale()
        {
            if (_asteroidModel == null)
                return;

            float scale = _asteroidModel.Size switch
            {
                AsteroidSize.Large => _largeSize,
                AsteroidSize.Medium => _mediumSize,
                AsteroidSize.Small => _smallSize,
                _ => _smallSize
            };

            transform.localScale = Vector3.one * scale;
        }
    }
}