using Game.Core.Input;
using UnityEngine;

namespace Game.Presentation.Input
{
    public class VirtualJoystickInput : IShipInput
    {
        private VirtualJoystickView _joystickView;
        private ActionButtonView _fireButton;
        private ActionButtonView _laserButton;

        public VirtualJoystickInput(
            VirtualJoystickView joystickView,
            ActionButtonView fireButton,
            ActionButtonView laserButton)
        {
            _joystickView = joystickView;
            _fireButton = fireButton;
            _laserButton = laserButton;
        }

        public float Thrust
        {
            get
            {
                var input = _joystickView != null ? _joystickView.InputVector : Vector2.zero;

                Debug.Log($"[Joystick] Input: {input}");

                return Mathf.Clamp01(input.y > 0f ? input.y : 0f);
            }
        }

        public float Turn
        {
            get
            {
                var input = _joystickView != null ? _joystickView.InputVector : Vector2.zero;
                return Mathf.Clamp(-input.x, -1f, 1f);
            }
        }

        public bool IsFirePressed => _fireButton != null && _fireButton.ConsumePress();
        public bool IsLaserPressed => _laserButton != null && _laserButton.ConsumePress();
    }
}