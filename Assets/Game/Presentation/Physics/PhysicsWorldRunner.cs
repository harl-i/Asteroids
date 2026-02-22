using Game.Infrastructure.Physics;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Physics
{
    public class PhysicsWorldRunner : MonoBehaviour
    {
        private PhysicsWorldProvider _provider;

        [Inject]
        public void Construct(PhysicsWorldProvider provider)
        {
            _provider = provider;
        }

        private void Update()
        {
            _provider.World.Tick(Time.deltaTime);
        }
    }
}