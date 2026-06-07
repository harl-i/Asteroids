using Game.Core.Signals;
using Game.Infrastructure.Weapons;
using Game.Presentation.Weapons;
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
        Container.BindInterfacesAndSelfTo<BulletRegistry>().AsSingle();
        Container.BindInterfacesAndSelfTo<BulletShooter>().AsSingle();
        Container.Bind<BulletFactory>().AsSingle();
        Container.Bind<BulletViewFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<BulletCollisionHandler>().AsSingle();
        Container.Bind<BulletPool>().AsSingle();
    }

    private void BindLaser()
    {
        Container.BindInterfacesAndSelfTo<LaserState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LaserShooter>().AsSingle();
    }
}
