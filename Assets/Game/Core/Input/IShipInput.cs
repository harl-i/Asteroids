namespace Game.Core.Input
{
    public interface IShipInput
    {
        public float Thrust { get; }
        public float Turn { get; }
        public bool IsFirePressed { get; }
    }
}