using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        InputInstaller.Install(Container);
        ShipInstaller.Install(Container);
        EnemyInstaller.Install(Container);
        WeaponsInstaller.Install(Container);
        UIInstaller.Install(Container);
        GameStateInstaller.Install(Container);
    }
}
