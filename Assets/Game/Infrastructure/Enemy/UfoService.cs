using System.Collections.Generic;
using Game.Core.Enemy;
using Zenject;

namespace Game.Infrastructure.Enemy
{
    public class UfoService : ITickable
    {
        private List<UfoModel> _ufos = new List<UfoModel>();

        public IReadOnlyList<UfoModel> Ufos => _ufos;
        public int ActiveCount => _ufos.Count;

        public void Add(UfoModel ufo)
        {
            if (ufo == null)
                return;

            _ufos.Add(ufo);
        }

        public void Tick()
        {
            for (int i = _ufos.Count - 1; i >= 0; i--)
            {
                if (!_ufos[i].Entity.IsActive)
                {
                    _ufos.RemoveAt(i);
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _ufos.Count; i++)
            {
                _ufos[i].Destroy();
            }

            _ufos.Clear();
        }
    }
}