using UnityEngine;

namespace Game.Core.UI
{
    public class ShipHudViewModel
    {
        public Vector2 Position { get; private set; }
        public float RotationDeg { get; private set; }
        public float Speed { get; private set; }
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public int LaserCharges { get; private set; }
        public int LaserMaxCharges { get; private set; }
        public float LaserCooldownRemaining { get; private set; }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetRotation(float rotationDeg)
        {
            RotationDeg = rotationDeg;
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }

        public void SetHealth(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        public void SetLaserState(int currentCharges, int maxCharges, float cooldownRemaining)
        {
            LaserCharges = currentCharges;
            LaserMaxCharges = maxCharges;
            LaserCooldownRemaining = cooldownRemaining;
        }
    }
}