namespace Game.Core.Services
{
    public interface IAdService
    {
        public void ShowInterstitial();
        public void ShowRewarded(System.Action onRewarded);
    }
}