using Game.Core.Input;
using Game.Infrastructure.Debug;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Ship;
using Game.Infrastructure.UI;
using Game.Presentation.Input;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameStartedSignal>();
        Container.DeclareSignal<EnemyKilledSignal>();
        Container.DeclareSignal<CollisionSignal>();

        Container.DeclareSignal<ShipDamagedSignal>();
        Container.DeclareSignal<ShipInvulnerabilityStartedSignal>();
        Container.DeclareSignal<ShipInvulnerabilityEndedSignal>();
        Container.DeclareSignal<GameOverSignal>();

        Container.BindInterfacesAndSelfTo<ConfigService>().AsSingle();
        Container.BindInterfacesAndSelfTo<PhysicsWorldProvider>().AsSingle();

        Container.Bind<IShipInput>().To<KeyboardInput>().AsSingle();

        Container.Bind<ShipFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShipControllerService>().AsSingle();
        Container.Bind<ZenjectCollisionEvents>().AsSingle();

        Container.BindInterfacesAndSelfTo<ShipDamageService>().AsSingle();

        Container.BindInterfacesAndSelfTo<ShipHudPresenter>().AsSingle();

        //Container.BindInterfacesAndSelfTo<DebugInitializer>().AsSingle();
        //Container.BindInterfacesTo<TestPhysicsBootstrap>().AsSingle();
        //Container.BindInterfacesTo<CollisionTest>().AsSingle();
        Container.BindInterfacesTo<ShipDebugListener>().AsSingle();
    }
}
