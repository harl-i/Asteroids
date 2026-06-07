using Cysharp.Threading.Tasks;
using Game.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Input
{
    public class MobileInputVisibilityHandler : IInitializable
    {
        private ConfigRepository _config;
        private MobileInputSceneRefs _refs;

        public MobileInputVisibilityHandler(
            ConfigRepository config,
            MobileInputSceneRefs refs)
        {
            _config = config;
            _refs = refs;
        }

        public async void Initialize()
        {
            await _config.LoadAsync();
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            bool isMobile = _config.InputConfig != null &&
                            _config.InputConfig.UseMobileInput;

            if (_refs != null && _refs.gameObject != null)
            {
                _refs.gameObject.SetActive(isMobile);
            }

            Debug.Log($"[MobileUI] Active: {isMobile}");
        }
    }
}
