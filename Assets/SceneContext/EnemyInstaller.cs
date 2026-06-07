using Game.Core.Signals;
using Game.Infrastructure.Enemies;
using Game.Infrastructure.Enemy;
using Game.Presentation.Enemy;
using Zenject;

public class EnemyInstaller : Installer<EnemyInstaller>
{
    public override void InstallBindings()
    {
        DeclareSignals();
        BindAsteroids();
        BindUfo();
        BindServices();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<EnemyKilledSignal>();
        Container.DeclareSignal<EnemyDeathRequestedSignal>();
    }

    private void BindAsteroids()
    {
        Container.Bind<AsteroidFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();
    }

    private void BindUfo()
    {
        Container.Bind<UfoFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<UfoSpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<UfoMovementController>().AsSingle();
    }

    private void BindServices()
    {
        Container.Bind<SpawnPositionProvider>().AsSingle();
        Container.Bind<AsteroidSplitter>().AsSingle();
        Container.Bind<AsteroidViewFactory>().AsSingle();
        Container.Bind<UfoViewFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyDeathHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyRegistry>().AsSingle();
    }
}
