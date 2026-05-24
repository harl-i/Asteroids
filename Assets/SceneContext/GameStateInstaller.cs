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
        Container.BindInterfacesAndSelfTo<ConfigService>().AsSingle();
        Container.BindInterfacesAndSelfTo<PhysicsWorldProvider>().AsSingle();
        Container.Bind<ZenjectCollisionEvents>().AsSingle();
    }

    private void BindGameState()
    {
        Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle();
        Container.BindInterfacesAndSelfTo<WorldResetService>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameOverHandler>().AsSingle();
    }

    private void BindExternalServices()
    {
        Container.Bind<IAdService>().To<YandexAdService>().AsSingle();
        Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();
    }

    private void BindDebug()
    {
        Container.BindInterfacesTo<ShipDebugListener>().AsSingle();
    }
}
