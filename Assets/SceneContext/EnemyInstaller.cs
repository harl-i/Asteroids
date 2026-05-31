using Game.Core.Signals;
using Game.Infrastructure.Enemies;
using Game.Infrastructure.Enemy;
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
        Container.BindInterfacesAndSelfTo<AsteroidSpawnService>().AsSingle();
    }

    private void BindUfo()
    {
        Container.BindInterfacesAndSelfTo<UfoService>().AsSingle();
        Container.Bind<UfoFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<UfoSpawnService>().AsSingle();
        Container.BindInterfacesAndSelfTo<UfoMovementService>().AsSingle();
    }

    private void BindServices()
    {
        Container.Bind<SpawnPositionService>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyDeathService>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyService>().AsSingle();
    }
}
