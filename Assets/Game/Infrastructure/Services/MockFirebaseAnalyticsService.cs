namespace Game.Infrastructure.Services
{
    public class MockFirebaseAnalyticsService : IFirebaseAnalyticsService
    {
        public void LogEvent(string eventName)
        {
            UnityEngine.Debug.Log($"[Analytics] Event: {eventName}");
        }

        public void LogEvent(string eventName, string param, object value)
        {
            UnityEngine.Debug.Log($"[Analytics] Event: {eventName} | {param}: {value}");
        }
    }
}