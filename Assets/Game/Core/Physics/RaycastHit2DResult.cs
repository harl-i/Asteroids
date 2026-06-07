using UnityEngine;

namespace Game.Core.Physics
{
    public struct RaycastHit2DResult
    {
        public Physics2DEntity Entity { get; }
        public Vector2 Point { get; }
        public float Distance { get; }

        public RaycastHit2DResult(Physics2DEntity entity, Vector2 point, float distance)
        {
            Entity = entity;
            Point = point;
            Distance = distance;
        }
    }
}
