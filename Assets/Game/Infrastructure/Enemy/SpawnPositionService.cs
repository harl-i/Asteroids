using Game.Core.World;
using Game.Infrastructure.Services;
using UnityEngine;

namespace Game.Infrastructure.Enemy
{
    public class SpawnPositionService
    {
        private ConfigService _config;

        public SpawnPositionService(ConfigService config)
        {
            _config = config;
        }

        public Vector2 GetPositionOutside(WorldBounds bounds)
        {
            float padding = _config.EnemyConfig.SpawnPadding;
            int side = Random.Range(0, 4);

            return side switch
            {
                0 => new Vector2(Random.Range(bounds.MinX, bounds.MaxX), bounds.MaxY + padding),
                1 => new Vector2(Random.Range(bounds.MinX, bounds.MaxX), bounds.MinY - padding),
                2 => new Vector2(bounds.MinX - padding, Random.Range(bounds.MinY, bounds.MaxY)),
                _ => new Vector2(bounds.MaxX + padding, Random.Range(bounds.MinY, bounds.MaxY))
            };
        }
    }
}
