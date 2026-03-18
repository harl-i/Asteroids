using UnityEngine;

namespace Game.Core.Physics
{
    public struct RaycastHit2DResult
    {
        public Physics2DEntity Entity;
        public Vector2 Point;
        public float Distance;

        public RaycastHit2DResult(Physics2DEntity entity, Vector2 point, float distance)
        {
            Entity = entity;
            Point = point;
            Distance = distance;
        }
    }
}