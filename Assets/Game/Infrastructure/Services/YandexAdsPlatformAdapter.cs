using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Game.Infrastructure.Services
{
    public class YandexAdsPlatformAdapter : IYandexAdsPlatformAdapter
    {
        private YandexBridge _bridge;

        [DllImport("__Internal")]
        private static extern void ShowInterstitialAd();

        [DllImport("__Internal")]
        private static extern void ShowRewardedAd();

        public YandexAdsPlatformAdapter(YandexBridge bridge)
        {
            _bridge = bridge;
        }

        public void ShowInterstitial()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            ShowInterstitialAd();
#else
            Debug.Log("[YandexAds] Interstitial (mock in editor)");
#endif
        }

        public void ShowRewarded(Action onRewarded)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _bridge.SetRewardedCallback(onRewarded);
            ShowRewardedAd();
#else
            Debug.Log("[YandexAds] Rewarded (mock in editor)");
            onRewarded?.Invoke();
#endif
        }
    }
}
