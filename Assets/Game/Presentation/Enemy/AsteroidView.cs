using Game.Core.Enemy;
using Game.Infrastructure.Services;
using Game.Presentation.Common;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Enemy
{
    public class AsteroidView : MonoBehaviour, IBindableView<AsteroidModel>
    {
        private AsteroidModel _asteroidModel;
        private ConfigService _config;

        [Inject]
        public void Construct(ConfigService config)
        {
            _config = config;
        }

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
                AsteroidSize.Large => _config.ViewConfig.Asteroid.LargeScale,
                AsteroidSize.Medium => _config.ViewConfig.Asteroid.MediumScale,
                AsteroidSize.Small => _config.ViewConfig.Asteroid.SmallScale,
                _ => _config.ViewConfig.Asteroid.SmallScale
            };

            transform.localScale = Vector3.one * scale;
        }
    }
}
