using Game.Core.Game;
using Game.Core.Input;
using Game.Core.Services;
using Game.Core.Signals;
using Game.Infrastructure.Debug;
using Game.Infrastructure.Enemies;
using Game.Infrastructure.Enemy;
using Game.Infrastructure.Game;
using Game.Infrastructure.Input;
using Game.Infrastructure.Physics;
using Game.Infrastructure.Score;
using Game.Infrastructure.Services;
using Game.Infrastructure.Ship;
using Game.Infrastructure.UI;
using Game.Infrastructure.Weapons;
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

        Container.DeclareSignal<ScoreChangedSignal>();

        Container.DeclareSignal<LaserChargesChangedSignal>();
        Container.DeclareSignal<LaserFiredSignal>();

        Container.DeclareSignal<EnemyDeathRequestedSignal>();

        Container.DeclareSignal<RestartGameSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();

        Container.DeclareSignal<ShipChangedSignal>();

        Container.BindInterfacesAndSelfTo<ConfigService>().AsSingle();
        Container.BindInterfacesAndSelfTo<PhysicsWorldProvider>().AsSingle();

        //Container.Bind<IShipInput>().To<KeyboardInput>().AsSingle();

        Container.Bind<ShipFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<ShipControllerService>().AsSingle();
        Container.Bind<ZenjectCollisionEvents>().AsSingle();

        Container.BindInterfacesAndSelfTo<ShipDamageService>().AsSingle();

        Container.BindInterfacesAndSelfTo<ShipHudPresenter>().AsSingle();

        Container.BindInterfacesAndSelfTo<BulletService>().AsSingle();
        Container.Bind<BulletFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<BulletCollisionService>().AsSingle();

        Container.Bind<BulletPool>().AsSingle();

        Container.Bind<AsteroidFactory>().AsSingle();
        //Container.BindInterfacesAndSelfTo<AsteroidDestructionService>().AsSingle();

        //Container.BindInterfacesAndSelfTo<AsteroidService>().AsSingle();
        Container.BindInterfacesAndSelfTo<AsteroidSpawnService>().AsSingle();

        Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();

        Container.BindInterfacesAndSelfTo<LaserService>().AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyDeathService>().AsSingle();

        Container.BindInterfacesAndSelfTo<UfoService>().AsSingle();
        Container.Bind<UfoFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<UfoSpawnService>().AsSingle();
        Container.BindInterfacesAndSelfTo<UfoMovementService>().AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyService>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle();
        Container.BindInterfacesAndSelfTo<WorldResetService>().AsSingle();
        Container.BindInterfacesAndSelfTo<RestartInputService>().AsSingle();
        Container.Bind<GameFacade>().AsSingle();

        Container.Bind<MobileInputSceneRefs>().FromComponentInHierarchy().AsSingle();

        Container.Bind<KeyboardInput>().AsSingle();

        Container.Bind<VirtualJoystickInput>().FromMethod(ctx =>
        {
            var refs = ctx.Container.Resolve<MobileInputSceneRefs>();
            return new VirtualJoystickInput(
                refs.JoystickView,
                refs.FireButton,
                refs.LaserButton);
        }).AsSingle();

        //Container.Bind<IShipInput>().To<InputRouter>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputRouter>().AsSingle();

        Container.Bind<IAdService>().To<MockAdService>().AsSingle();

        Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();
        //Container.Bind<IFirebaseAnalyticsService>().To<FirebaseAnalyticsService>().FromResolve();

        //Container.BindInterfacesAndSelfTo<DebugInitializer>().AsSingle();
        //Container.BindInterfacesTo<TestPhysicsBootstrap>().AsSingle();
        //Container.BindInterfacesTo<CollisionTest>().AsSingle();
        Container.BindInterfacesTo<ShipDebugListener>().AsSingle();
    }
}
