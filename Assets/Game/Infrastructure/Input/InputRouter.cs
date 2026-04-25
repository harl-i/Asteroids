using Game.Core.Input;
using Game.Infrastructure.Services;
using Game.Presentation.Input;
using Zenject;

namespace Game.Infrastructure.Input
{
    public class InputRouter : IShipInput, IInitializable
    {
        private KeyboardInput _keyboardInput;
        private VirtualJoystickInput _virtualJoystickInput;
        private ConfigService _configService;

        private bool _useMobileInput;

        public InputRouter(
            KeyboardInput keyboardInput,
            VirtualJoystickInput virtualJoystickInput,
            ConfigService configService)
        {
            _keyboardInput = keyboardInput;
            _virtualJoystickInput = virtualJoystickInput;
            _configService = configService;
        }

        private IShipInput ActiveInput => _useMobileInput
            ? _virtualJoystickInput
            : _keyboardInput;

        public float Thrust => ActiveInput.Thrust;
        public float Turn => ActiveInput.Turn;
        public bool IsFirePressed => ActiveInput.IsFirePressed;
        public bool IsLaserPressed => ActiveInput.IsLaserPressed;

        public void Initialize()
        {
            _useMobileInput = _configService.PlayerConfig.useMobileInput;
        }
    }
}