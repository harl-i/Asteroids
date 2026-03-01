using UnityEngine;

namespace Game.Core.Physics
{
    public static class CollisionDetector2D
    {
        private static float _minDist = 0.000001f;

        public static bool TryCircleCircle(Physics2DEntity a, Physics2DEntity b, out CollisionManifold collisionManifold)
        {
            collisionManifold = default;

            Vector2 delta = b.Position - a.Position;
            float distSqr = delta.sqrMagnitude;
            float radius = a.Radius + b.Radius;

            if (distSqr >= radius * radius) return false;

            float dist = Mathf.Sqrt(Mathf.Max(distSqr, _minDist));
            Vector2 normal = delta / dist;
            float penetration = radius - dist;

            collisionManifold = new CollisionManifold(a, b, normal, penetration);
            return true;
        }
    }
}