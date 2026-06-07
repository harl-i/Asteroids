using Cysharp.Threading.Tasks;
using Game.Core.Input;
using Game.Infrastructure.Services;
using Game.Presentation.Input;
using Zenject;

namespace Game.Infrastructure.Input
{
    public class InputRouter : IShipInput, IGameInput, IInitializable
    {
        private KeyboardInput _keyboardInput;
        private VirtualJoystickInput _virtualJoystickInput;
        private ConfigRepository _configRepository;

        private bool _useMobileInput;

        public InputRouter(
            KeyboardInput keyboardInput,
            VirtualJoystickInput virtualJoystickInput,
            ConfigRepository configRepository)
        {
            _keyboardInput = keyboardInput;
            _virtualJoystickInput = virtualJoystickInput;
            _configRepository = configRepository;
        }

        private IGameInput ActiveInput => _useMobileInput
            ? _virtualJoystickInput
            : _keyboardInput;

        public float Thrust => ActiveInput.Thrust;
        public float Turn => ActiveInput.Turn;
        public bool IsFirePressed => ActiveInput.IsFirePressed;
        public bool IsLaserPressed => ActiveInput.IsLaserPressed;
        public bool IsRestartPressed => ActiveInput.IsRestartPressed;

        public async void Initialize()
        {
            await _configRepository.LoadAsync();
            _useMobileInput = _configRepository.InputConfig.UseMobileInput;
        }
    }
}
