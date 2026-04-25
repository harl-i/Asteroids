using Game.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Input
{
    public class MobileInputVisibilityService : IInitializable
    {
        private ConfigService _config;
        private MobileInputSceneRefs _refs;

        public MobileInputVisibilityService(
            ConfigService config,
            MobileInputSceneRefs refs)
        {
            _config = config;
            _refs = refs;
        }

        public void Initialize()
        {
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            bool isMobile = _config.PlayerConfig != null &&
                            _config.PlayerConfig.useMobileInput;

            if (_refs != null && _refs.gameObject != null)
            {
                _refs.gameObject.SetActive(isMobile);
            }

            Debug.Log($"[MobileUI] Active: {isMobile}");
        }
    }
}
