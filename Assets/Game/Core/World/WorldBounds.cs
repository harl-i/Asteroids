using UnityEngine;

namespace Game.Core.World
{
    public struct WorldBounds
    {
        public float MinX;
        public float MaxX;
        public float MinY;
        public float MaxY;

        public float Width => MaxX - MinX;
        public float Height => MaxY - MinY;

        public WorldBounds(float minX, float maxX, float minY, float maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        public Vector2 Wrap(Vector2 position)
        {
            float x = position.x;
            float y = position.y;

            if (x < MinX) x += Width;
            else if (x > MaxX) x -= Width;

            if (y < MinY) y += Height;
            else if (y > MaxY) y -= Height;

            return new Vector2(x, y);
        }
    }
}