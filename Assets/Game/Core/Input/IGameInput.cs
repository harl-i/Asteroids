namespace Game.Core.Input
{
    public interface IGameInput : IShipInput
    {
       public bool IsRestartPressed { get; }
    }
}
