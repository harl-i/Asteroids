using System.Collections.Generic;
using UnityEngine;

namespace Game.Presentation.Common
{
    public class ModelViewFactory<TModel, TView>
        where TView : MonoBehaviour, IBindableView<TModel>
    {
        private Dictionary<TModel, TView> _views = new Dictionary<TModel, TView>();
        private List<TModel> _modelsToRemove = new List<TModel>();

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

        public void CleanupInactive(System.Predicate<TModel> isActive)
        {
            _modelsToRemove.Clear();

            foreach (var pair in _views)
            {
                if (isActive(pair.Key))
                    continue;

                if (pair.Value != null)
                    Object.Destroy(pair.Value.gameObject);

                _modelsToRemove.Add(pair.Key);
            }

            for (int i = 0; i < _modelsToRemove.Count; i++)
            {
                _views.Remove(_modelsToRemove[i]);
            }
        }
    }
}
