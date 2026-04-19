using UnityEngine;

namespace Game.Presentation.Input
{
    public class MobileInputSceneRefs : MonoBehaviour
    {
        [SerializeField] private VirtualJoystickView _joystickView;
        [SerializeField] private ActionButtonView _fireButton;
        [SerializeField] private ActionButtonView _laserButton;

        public VirtualJoystickView JoystickView => _joystickView;
        public ActionButtonView FireButton => _fireButton;
        public ActionButtonView LaserButton => _laserButton;
    }
}