namespace Game.Core.Signals
{
    public struct LaserChargesChangedSignal
    {
        public int CurrentCharges;
        public int MaxCharges;
        public float CooldownRemaining;
    }
}