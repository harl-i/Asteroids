using UnityEngine;

namespace Game.Core.Physics
{
    public static class Physics2DIntegrator
    {
        public static void Integrate(Physics2DEntity entity, float dt)
        {
            if (!entity.IsActive) return;

            Vector2 acceleration = entity.AccumulatedForce / entity.Mass;
            entity.Velocity += acceleration * dt;
            entity.Position += entity.Velocity * dt;

            entity.ClearForces();
        }
    }
}