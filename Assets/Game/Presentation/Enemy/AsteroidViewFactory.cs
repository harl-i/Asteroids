using System.Collections.Generic;
using Game.Core.Enemy;
using UnityEngine;

namespace Game.Presentation.Enemy
{
    public class AsteroidViewFactory
    {
        private AsteroidView _prefab;
        private Transform _parent;
        private Dictionary<AsteroidModel, AsteroidView> _views = new Dictionary<AsteroidModel, AsteroidView>();

        public AsteroidViewFactory(AsteroidView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public AsteroidView GetOrCreate(AsteroidModel asteroid)
        {
            if (_views.TryGetValue(asteroid, out var existing))
                return existing;

            AsteroidView view = Object.Instantiate(_prefab, _parent);
            view.Bind(asteroid);

            _views.Add(asteroid, view);
            return view;
        }
    }
}