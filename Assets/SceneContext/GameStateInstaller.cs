using Game.Core.Services;
using Game.Core.Signals;
using Game.Infrastructure.Debug;
using Game.Infrastructure.Game;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Services;
using Zenject;

public class GameStateInstaller : Installer<GameStateInstaller>
{
    public override void InstallBindings()
    {
        DeclareSignals();
        BindInfrastructure();
        BindGameState();
        BindExternalServices();
        BindDebug();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<GameOverSignal>();
        Container.DeclareSignal<RestartGameSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();
        Container.DeclareSignal<CollisionSignal>();
    }

    private void BindInfrastructure()
    {
        Container.BindInterfacesAndSelfTo<ConfigRepository>().AsSingle();
        Container.BindInterfacesAndSelfTo<PhysicsWorldProvider>().AsSingle();
        Container.Bind<ZenjectCollisionEvents>().AsSingle();

        Container.BindExecutionOrder<ConfigRepository>(-100);
        Container.BindExecutionOrder<PhysicsWorldProvider>(-90);
    }

    private void BindGameState()
    {
        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        Container.BindInterfacesAndSelfTo<WorldResetHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShipResetHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<RestartAnalyticsHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameOverAnalyticsHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameOverAdHandler>().AsSingle();
    }

    private void BindExternalServices()
    {
        Container.Bind<YandexBridge>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IYandexAdsPlatformAdapter>().To<YandexAdsPlatformAdapter>().AsSingle();
        Container.Bind<IAdProvider>().To<YandexAdProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<FirebaseAnalyticsTracker>().AsSingle();
    }

    private void BindDebug()
    {
        Container.BindInterfacesTo<ShipDebugListener>().AsSingle();
    }
}
