using Game.Core.Signals;
using Game.Core.UI;
using Game.Infrastructure.Score;
using Game.Infrastructure.UI;
using Zenject;

public class UIInstaller : Installer<UIInstaller>
{
    public override void InstallBindings()
    {
        DeclareSignals();

        Container.BindInterfacesAndSelfTo<ScoreTracker>().AsSingle();
        Container.Bind<ShipHudViewModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShipHudPresenter>().AsSingle();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<ScoreChangedSignal>();
    }
}
