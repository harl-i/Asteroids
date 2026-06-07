namespace Game.Core.Services
{
    public interface IAdProvider
    {
        public void ShowInterstitial();
        public void ShowRewarded(System.Action onRewarded);
    }
}