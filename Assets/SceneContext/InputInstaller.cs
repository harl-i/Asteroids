using Game.Infrastructure.Game;
using Game.Infrastructure.Input;
using Game.Presentation.Input;
using Zenject;

public class InputInstaller : Installer<InputInstaller>
{
    public override void InstallBindings()
    {
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

        Container.BindInterfacesAndSelfTo<MobileInputVisibilityHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputRouter>().AsSingle();
        Container.BindInterfacesAndSelfTo<RestartInputHandler>().AsSingle();
    }
}
