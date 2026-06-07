using Game.Core.Signals;
using Game.Infrastructure.Ship;
using Zenject;

public class ShipInstaller : Installer<ShipInstaller>
{
    public override void InstallBindings()
    {
        DeclareSignals();
        BindServices();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<ShipChangedSignal>();
        Container.DeclareSignal<ShipDamagedSignal>();
        Container.DeclareSignal<ShipInvulnerabilityStartedSignal>();
        Container.DeclareSignal<ShipInvulnerabilityEndedSignal>();
    }

    private void BindServices()
    {
        Container.Bind<ShipFactory>().AsSingle();
        Container.Bind<ShipService>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShipMovementService>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShipDamageService>().AsSingle();
    }
}
