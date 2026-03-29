using System.Collections.Generic;
using Game.Core.Enemy;
using UnityEngine;

namespace Game.Presentation.Enemy
{
    public class UfoViewFactory
    {
        private UfoView _prefab;
        private Transform _parent;
        private Dictionary<UfoModel, UfoView> _views = new Dictionary<UfoModel, UfoView>();

        public UfoViewFactory(UfoView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public UfoView GetOrCreate(UfoModel ufo)
        {
            if (_views.TryGetValue(ufo, out var existing))
                return existing;

            UfoView view = Object.Instantiate(_prefab, _parent);
            view.Bind(ufo);

            _views.Add(ufo, view);
            return view;
        }
    }
}