using System;

namespace Game.Infrastructure.Services
{
    public interface IYandexAdsPlatformAdapter
    {
       public void ShowInterstitial();
       public void ShowRewarded(Action onRewarded);
    }
}
