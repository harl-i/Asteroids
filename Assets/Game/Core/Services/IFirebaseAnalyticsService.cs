public interface IFirebaseAnalyticsService
{
    public void LogEvent(string eventName);
    public void LogEvent(string eventName, string param, object value);

}