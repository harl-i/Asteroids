using Game.Core.Enemy;
using Game.Infrastructure.Enemy;
using Game.Infrastructure.Services;
using UnityEngine;

namespace Game.Infrastructure.Enemies
{
    public class AsteroidSplitService
    {
        private AsteroidFactory _asteroidFactory;
        private ConfigService _configService;

        public AsteroidSplitService(
            AsteroidFactory asteroidFactory,
            ConfigService configService)
        {
            _asteroidFactory = asteroidFactory;
            _configService = configService;
        }

        public void Split(AsteroidModel asteroid)
        {
            if (asteroid.Size == AsteroidSize.Small)
                return;

            AsteroidSize newSize = asteroid.Size == AsteroidSize.Large
                ? AsteroidSize.Medium
                : AsteroidSize.Small;

            Vector2 position = asteroid.Entity.Position;

            AsteroidModel firstFragment = _asteroidFactory.Create(position, newSize);
            AsteroidModel secondFragment = _asteroidFactory.Create(position, newSize);

            float fragmentSpeed = _configService.EnemyConfig.AsteroidFragmentSpeed;
            firstFragment.Entity.SetVelocity(Random.insideUnitCircle * fragmentSpeed);
            secondFragment.Entity.SetVelocity(Random.insideUnitCircle * fragmentSpeed);
        }
    }
}
