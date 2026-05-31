using UnityEngine;

namespace Game.Core.Physics
{
    public class Physics2DEntity
    {
        private const float MinMass = 0.0001f;

        public int Id { get; }

        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public Vector2 AccumulatedForce { get; private set; }

        public CollisionLayer CollisionLayer { get; private set; }
        public object PhysicsOwner { get; private set; }
        public float Restitution { get; private set; }

        public float Mass { get; }
        public float Radius { get; }
        public bool IsTrigger { get; private set; }
        public bool IsActive { get; private set; }

        public Physics2DEntity (
            int id, Vector2 position,
            float radius,
            float mass)
        {
            Id = id;
            Position = position;
            Radius = radius;
            Mass = Mathf.Max(MinMass, mass);

            Velocity = Vector2.zero;
            AccumulatedForce = Vector2.zero;
            IsTrigger = false;
            IsActive = true;

            CollisionLayer = CollisionLayer.Enemy;
            PhysicsOwner = null;
            Restitution = 1f;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void Translate(Vector2 delta)
        {
            Position += delta;
        }

        public void SetVelocity(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public void AddVelocity(Vector2 delta)
        {
            Velocity += delta;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void ConfigureCollision(CollisionLayer layer, float restitution, bool isTrigger = false)
        {
            CollisionLayer = layer;
            Restitution = Mathf.Clamp01(restitution);
            IsTrigger = isTrigger;
        }

        public void SetPhysicsOwner(object owner)
        {
            PhysicsOwner = owner;
        }

        public void AddForce(Vector2 force)
        {
            AccumulatedForce += force;
        }

        public void ClearForces()
        {
            AccumulatedForce = Vector2.zero;
        }
    }
}
