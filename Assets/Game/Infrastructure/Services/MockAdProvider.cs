using System;
using Game.Core.Services;

namespace Game.Infrastructure.Services
{
    public class MockAdProvider : IAdProvider
    {
        public void ShowInterstitial()
        {
            UnityEngine.Debug.Log("[ADS] Show Interstitial");
        }

        public void ShowRewarded(Action onRewarded)
        {
            UnityEngine.Debug.Log("[ADS] Show Rewarded");

            onRewarded?.Invoke();
        }
    }
}