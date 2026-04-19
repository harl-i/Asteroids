using System;
using Game.Core.Services;

namespace Game.Infrastructure.Services
{
    public class MockAdService : IAdService
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