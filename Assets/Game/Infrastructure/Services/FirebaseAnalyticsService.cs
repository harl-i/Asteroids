using Firebase;
using Firebase.Analytics;
using Zenject;

namespace Game.Infrastructure.Services
{
    public class FirebaseAnalyticsService : IFirebaseAnalyticsService, IInitializable
    {
        private bool _isInitialized;

        public void Initialize()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var status = task.Result;

                if (status == DependencyStatus.Available)
                {
                    _isInitialized = true;
                    UnityEngine.Debug.Log("[Firebase] Initialized");
                }
                else
                {
                    UnityEngine.Debug.LogError($"[Firebase] Failed: {status}");
                }
            });
        }

        public void LogEvent(string eventName)
        {
            if (!_isInitialized) return;

            FirebaseAnalytics.LogEvent(eventName);
        }

        public void LogEvent(string eventName, string param, object value)
        {
            if (!_isInitialized) return;

            FirebaseAnalytics.LogEvent(eventName,
                new Parameter(param, value.ToString()));
        }
    }
}