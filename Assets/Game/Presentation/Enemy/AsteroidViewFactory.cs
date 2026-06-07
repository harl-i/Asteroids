using System.Collections.Generic;
using Game.Core.Enemy;
using UnityEngine;

namespace Game.Presentation.Enemy
{
    public class AsteroidViewFactory
    {
        private Dictionary<AsteroidModel, AsteroidView> _views = new Dictionary<AsteroidModel, AsteroidView>();

        public AsteroidView GetOrCreate(AsteroidModel asteroid, AsteroidView prefab, Transform parent)
        {
            if (_views.TryGetValue(asteroid, out var existing))
                return existing;

            AsteroidView view = Object.Instantiate(prefab, parent);
            view.Bind(asteroid);

            _views.Add(asteroid, view);
            return view;
        }
    }
}
