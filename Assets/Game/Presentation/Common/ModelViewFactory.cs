using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation.Common
{
    public class ModelViewFactory<TModel, TView>
        where TView : MonoBehaviour, IBindableView<TModel>
    {
        private Dictionary<TModel, TView> _views = new Dictionary<TModel, TView>();

        public TView GetOrCreate(TModel model, TView prefab, Transform parent)
        {
            if (_views.TryGetValue(model, out TView existing))
            {
                existing.gameObject.SetActive(true);
                return existing;
            }

            TView view = Object.Instantiate(prefab, parent);
            view.Bind(model);

            _views.Add(model, view);
            return view;
        }
    }
}
