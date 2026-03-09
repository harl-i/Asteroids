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

        public int MaxHealth { get; }
        public int CurrentHealth { get; private set; }

        public bool IsInvulnerable { get; private set; }
        public bool IsControlLocked { get; private set; }

        public ShipModel(Physics2DEntity entity, float acceleration, float turnSpeedDeg, int maxHealth)
        {
            Entity = entity;
            _acceleration = acceleration;
            _turnSpeedDeg = turnSpeedDeg;
            RotationDeg = 90f;
            CurrentHealth = maxHealth;
            MaxHealth = maxHealth;
        }

        public void Tick(float dt, float thrust, float thrustMinus)
        {
            if (IsControlLocked)
            {
                thrust = 0f;
                thrustMinus = 0f;
            }

            RotationDeg += thrustMinus * _turnSpeedDeg * dt;

            var rad = RotationDeg * Mathf.Deg2Rad;
            var forward = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            var clampThrust = Mathf.Clamp01(thrust);
            var force = forward * (_acceleration * Entity.Mass * clampThrust);

            Entity.AddForce(force);
        }

        public bool CanTakeDamage()
        {
            return !IsInvulnerable && CurrentHealth > 0;
        }

        public void ApplyDamage(int damage)
        {
            if (!CanTakeDamage()) return;

            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        }

        public void SetInvulnerable(bool value)
        {
            IsInvulnerable = value;
        }

        public void SetControlLocked(bool value)
        {
            IsControlLocked = value;
        }
    }
}