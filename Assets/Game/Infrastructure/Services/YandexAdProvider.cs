using System;
using Game.Core.Services;

namespace Game.Infrastructure.Services
{
    public class YandexAdProvider : IAdProvider
    {
        private IYandexAdsPlatformAdapter _platformAdapter;

        public YandexAdProvider(IYandexAdsPlatformAdapter platformAdapter)
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
