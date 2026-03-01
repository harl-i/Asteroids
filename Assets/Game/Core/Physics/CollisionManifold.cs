using UnityEngine;

namespace Game.Core.Physics
{
    public struct CollisionManifold
    {
        public readonly Physics2DEntity A;
        public readonly Physics2DEntity B;
        public readonly Vector2 Normal;
        public readonly float Penetration;

        public CollisionManifold(Physics2DEntity a, Physics2DEntity b, Vector2 normal, float penetration)
        {
            A = a;
            B = b;
            Normal = normal;
            Penetration = penetration;
        }
    }
}