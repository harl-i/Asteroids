using UnityEngine;

namespace Game.Core.Physics
{
    public static class ImpulseSolver2D
    {
        public static void Resolve(in CollisionManifold collisionManifold)
        {
            Physics2DEntity a = collisionManifold.A;
            Physics2DEntity b = collisionManifold.B;
            if (!a.IsActive || !b.IsActive) return;

            if (a.IsTrigger || b.IsTrigger) return;

            Vector2 normal = collisionManifold.Normal;

            Vector2 relativeVelocity = b.Velocity - a.Velocity;
            float velAlongNormal = Vector2.Dot(relativeVelocity, normal);

            if (velAlongNormal > 0f)
            {
                PositionalCorrection(a, b, normal, collisionManifold.Penetration);
                return;
            }

            float restitutionCoefficient = Mathf.Clamp01(Mathf.Min(a.Restitution, b.Restitution));

            float invMassA = 1f / a.Mass;
            float invMassB = 1f / b.Mass;

            float impulseScalar = -(1f + restitutionCoefficient) * velAlongNormal;
            impulseScalar /= (invMassA + invMassB);

            Vector2 impulse = impulseScalar * normal;
            a.Velocity -= impulse * invMassA;
            b.Velocity += impulse * invMassB;

            PositionalCorrection(a, b, normal, collisionManifold.Penetration);
        }

        private static void PositionalCorrection(Physics2DEntity a, Physics2DEntity b, Vector2 normal, float penetration)
        {
            const float percent = 0.8f;
            const float slop = 0.01f;

            float invMassA = 1f / a.Mass;
            float invMassB = 1f / b.Mass;

            float correctionMag = Mathf.Max(penetration - slop, 0f) / (invMassA + invMassB) * percent;
            Vector2 correction = correctionMag * normal;

            a.Position -= correction * invMassA;
            b.Position += correction * invMassB;
        }
    }
}