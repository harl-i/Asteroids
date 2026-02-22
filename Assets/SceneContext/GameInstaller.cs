using Game.Core.Input;
using Game.Infrastructure.Debug;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Ship;
using Game.Presentation.Input;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameStartedSignal>();
        Container.DeclareSignal<EnemyKilledSignal>();

        //Container.BindInterfacesAndSelfTo<PlayerConfig>().AsSingle();
        //Container.BindInterfacesAndSelfTo<WorldConfig>().AsSingle();
        Container.BindInterfacesAndSelfTo<ConfigService>().AsSingle();
        Container.BindInterfacesAndSelfTo<PhysicsWorldProvider>().AsSingle();

        Container.Bind<IShipInput>().To<KeyboardInput>().AsSingle();

        Container.Bind<ShipFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShipControllerService>().AsSingle();

        Container.BindInterfacesAndSelfTo<DebugInitializer>().AsSingle();
        Container.BindInterfacesTo<TestPhysicsBootstrap>().AsSingle();
    }
}
