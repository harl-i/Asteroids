using System.Collections.Generic;
using Game.Core.World;
using UnityEngine;

namespace Game.Core.Physics
{
    public class Physics2DWorld
    {
        private List<Physics2DEntity> _entities = new();
        private int _nextId = 1;

        private ICollisionEvents _collisionEvents;

        public WorldBounds Bounds { get; }

        public IReadOnlyList<Physics2DEntity> Entities => _entities;

        public Physics2DWorld(WorldBounds bounds, ICollisionEvents collisionEvents)
        {
            Bounds = bounds;
            _collisionEvents = collisionEvents;
        }

        public Physics2DEntity CreateEntity(
            Vector2 position,
            float radius,
            float mass)
        {
            Physics2DEntity entity = new Physics2DEntity(_nextId++, position, radius, mass);
            _entities.Add(entity);

            return entity;
        }

        public void RemoveEntity(Physics2DEntity entity)
        {
            _entities.Remove(entity);
        }

        public void Tick(float dt)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                Physics2DEntity entity = _entities[i];
                if (!entity.IsActive) continue;

                Physics2DIntegrator.Integrate(entity, dt);
                entity.Position = Bounds.Wrap(entity.Position);
            }

            for (int i = 0; i < _entities.Count; i++)
            {
                Physics2DEntity a = _entities[i];
                if (!a.IsActive) continue;

                for (int j = i + 1; j < _entities.Count; j++)
                {
                    Physics2DEntity b = _entities[j];
                    if (!b.IsActive) continue;

                    if (CollisionDetector2D.TryCircleCircle(a, b, out CollisionManifold collisionManifold))
                    {
                        ImpulseSolver2D.Resolve(collisionManifold);
                        _collisionEvents?.OnCollision(a, b);
                    }
                }
            }
        }
    }
}