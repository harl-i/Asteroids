using System.Collections.Generic;
using Game.Core.Weapons;
using UnityEngine;

namespace Game.Presentation.Weapons
{
    public class BulletViewFactory
    {
        private BulletView _prefab;
        private Transform _parent;
        private Dictionary<BulletModel, BulletView> _views = new Dictionary<BulletModel, BulletView>();

        public BulletViewFactory(BulletView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public BulletView GetOrCreate(BulletModel bullet)
        {
            if (_views.TryGetValue(bullet, out BulletView existing))
                return existing;

            BulletView view = Object.Instantiate(_prefab, _parent);
            view.Bind(bullet);

            _views.Add(bullet, view);
            return view;
        }
    }
}