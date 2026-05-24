using Game.Core.Signals;
using Game.Infrastructure.Weapons;
using Zenject;

public class WeaponsInstaller : Installer<WeaponsInstaller>
{
    public override void InstallBindings()
    {
        DeclareSignals();
        BindBullets();
        BindLaser();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<LaserChargesChangedSignal>();
        Container.DeclareSignal<LaserFiredSignal>();
    }

    private void BindBullets()
    {
        Container.BindInterfacesAndSelfTo<BulletService>().AsSingle();
        Container.Bind<BulletFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<BulletCollisionService>().AsSingle();
        Container.Bind<BulletPool>().AsSingle();
    }

    private void BindLaser()
    {
        Container.BindInterfacesAndSelfTo<LaserService>().AsSingle();
    }
}
