using UnityEngine;

namespace Game.Core.Physics
{
    public class Physics2DEntity
    {
        private float _minMass = 0.0001f;

        public int Id { get; }

        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 AccumulatedForce;

        public float Mass;
        public float Radius;
        public bool IsTrigger;
        public bool IsActive;

        public Physics2DEntity (int id, Vector2 position, float radius, float mass)
        {
            Id = id;
            Position = position;
            Radius = radius;
            Mass = Mathf.Max(_minMass, mass);

            Velocity = Vector2.zero;
            AccumulatedForce = Vector2.zero;
            IsTrigger = false;
            IsActive = true;
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