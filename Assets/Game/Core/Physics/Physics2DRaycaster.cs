using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Physics
{
    public static class Physics2DRaycaster
    {
        public static List<RaycastHit2DResult> RaycastAll(
            IEnumerable<Physics2DEntity> entities,
            Vector2 origin,
            Vector2 direction,
            float maxDistance)
        {
            List<RaycastHit2DResult> hits = new List<RaycastHit2DResult>();
            Vector2 normalizeDirection = direction.normalized;

            foreach (var entity in entities)
            {
                if (!entity.IsActive)
                    continue;

                if (entity.CollisionLayer != CollisionLayer.Enemy)
                    continue;

                if (TryRayCircle(origin, normalizeDirection, maxDistance, entity, out var hit))
                {
                    hits.Add(hit);
                }
            }

            hits.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            return hits;
        }

        private static bool TryRayCircle(
            Vector2 origin,
            Vector2 direction,
            float maxDistance,
            Physics2DEntity entity,
            out RaycastHit2DResult hit)
        {
            hit = default;

            Vector2 toCircle = entity.Position - origin;
            float projection = Vector2.Dot(toCircle, direction);

            if (projection < 0f || projection > maxDistance)
                return false;

            Vector2 closestPoint = origin + direction * projection;
            float distanceToCenter = Vector2.Distance(closestPoint, entity.Position);

            if (distanceToCenter > entity.Radius)
                return false;

            float offset = Mathf.Sqrt(entity.Radius * entity.Radius - distanceToCenter * distanceToCenter);
            float hitDistance = projection - offset;

            if (hitDistance < 0f || hitDistance > maxDistance)
                return false;

            Vector2 hitPoint = origin + direction * hitDistance;
            hit = new RaycastHit2DResult(entity, hitPoint, hitDistance);
            return true;
        }
    }
}