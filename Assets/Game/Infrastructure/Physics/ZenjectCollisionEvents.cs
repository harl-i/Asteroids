using Game.Core.Physics;
using Game.Core.Signals;
using Zenject;

namespace Game.Infrastructure.Physics
{
    public class ZenjectCollisionEvents : ICollisionEvents
    {
        private SignalBus _signalBus;

        public ZenjectCollisionEvents(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void OnCollision(Physics2DEntity a, Physics2DEntity b)
        {
            _signalBus.Fire(new CollisionSignal { A = a, B = b });
        }
    }
}