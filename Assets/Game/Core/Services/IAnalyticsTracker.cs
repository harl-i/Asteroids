namespace Game.Core.Services
{
    public interface IAnalyticsTracker
    {
        public void LogEvent(string eventName);
        public void LogEvent(string eventName, string param, object value);
    }
}
