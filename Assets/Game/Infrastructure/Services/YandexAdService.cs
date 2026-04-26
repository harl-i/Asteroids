using System;
using System.Runtime.InteropServices;
using Game.Core.Services;
using UnityEngine;

namespace Game.Infrastructure.Services
{
    public class YandexAdService : IAdService
    {
//#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void ShowInterstitialAd();

        [DllImport("__Internal")]
        private static extern void ShowRewardedAd();
//#endif

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
            YandexBridge.OnRewardedCallback = onRewarded;
            ShowRewardedAd();
#else
            Debug.Log("[YandexAds] Rewarded (mock in editor)");
            onRewarded?.Invoke();
#endif
        }
    }
}
