using System.Collections.Generic;
using Game.Core.Enemy;
using UnityEngine;

namespace Game.Presentation.Enemy
{
    public class UfoViewFactory
    {
        private Dictionary<UfoModel, UfoView> _views = new Dictionary<UfoModel, UfoView>();

        public UfoView GetOrCreate(UfoModel ufo, UfoView prefab, Transform parent)
        {
            if (_views.TryGetValue(ufo, out var existing))
                return existing;

            UfoView view = Object.Instantiate(prefab, parent);
            view.Bind(ufo);

            _views.Add(ufo, view);
            return view;
        }
    }
}
