using System;
using UnityEngine;

namespace Game.Infrastructure.Services
{
    public class YandexBridge : MonoBehaviour
    {
        public static Action OnRewardedCallback;

        public void OnRewarded()
        {
            Debug.Log("[YandexAds] Reward received");
            OnRewardedCallback?.Invoke();
            OnRewardedCallback = null;
        }
    }
}