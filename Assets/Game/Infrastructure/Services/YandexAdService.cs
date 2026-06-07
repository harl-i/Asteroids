using System;
using Game.Core.Services;

namespace Game.Infrastructure.Services
{
    public class YandexAdService : IAdService
    {
        private IYandexAdsPlatformAdapter _platformAdapter;

        public YandexAdService(IYandexAdsPlatformAdapter platformAdapter)
        {
            _platformAdapter = platformAdapter;
        }

        public void ShowInterstitial()
        {
            _platformAdapter.ShowInterstitial();
        }

        public void ShowRewarded(Action onRewarded)
        {
            _platformAdapter.ShowRewarded(onRewarded);
        }
    }
}
