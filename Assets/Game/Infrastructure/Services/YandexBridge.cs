using System;
using UnityEngine;

namespace Game.Infrastructure.Services
{
    public class YandexBridge : MonoBehaviour
    {
        private Action _onRewardedCallback;

        public void SetRewardedCallback(Action onRewardedCallback)
        {
            _onRewardedCallback = onRewardedCallback;
        }

        public void OnRewarded()
        {
            Debug.Log("[YandexAds] Reward received");
            _onRewardedCallback?.Invoke();
            _onRewardedCallback = null;
        }
    }
}
