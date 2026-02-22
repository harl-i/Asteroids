using Game.Core.Physics;
using UnityEngine;

namespace Game.Core.Ship
{
    public class ShipModel
    {
        private float _acceleration;
        private float _turnSpeedDeg;

        public Physics2DEntity Entity { get; }
        public float RotationDeg { get; private set; }

        public ShipModel(Physics2DEntity entity, float acceleration, float turnSpeedDeg)
        {
            Entity = entity;
            _acceleration = acceleration;
            _turnSpeedDeg = turnSpeedDeg;
            RotationDeg = 90f;
        }

        public void Tick(float dt, float thrust, float thrustMinus)
        {
            RotationDeg += thrustMinus * _turnSpeedDeg * dt;

            var rad = RotationDeg * Mathf.Deg2Rad;
            var forward = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            var clampThrust = Mathf.Clamp01(thrust);
            var force = forward * (_acceleration * Entity.Mass * clampThrust);

            Entity.AddForce(force);
        }
    }
}