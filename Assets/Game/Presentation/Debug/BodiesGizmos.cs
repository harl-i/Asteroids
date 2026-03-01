using Game.Infrastructure.Physics;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Debug
{
    public class BodiesGizmos : MonoBehaviour
    {
        private PhysicsWorldProvider _provider;

        [Inject]
        public void Construct(PhysicsWorldProvider provider) => _provider = provider;

        private void OnDrawGizmos()
        {
            if (_provider?.World == null) return;

            foreach (var entity in _provider.World.Entities)
            {
                if (!entity.IsActive) continue;
                Gizmos.DrawWireSphere(new Vector3(entity.Position.x, entity.Position.y, 0f), entity.Radius);
            }
        }
    }
}