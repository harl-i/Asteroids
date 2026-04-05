using Game.Core.Enemy;
using Game.Infrastructure.Enemy;
using UnityEngine;
using Zenject;

namespace Game.Presentation.Enemy
{
    public class UfoViewBootstrap : MonoBehaviour
    {
        [SerializeField] private UfoView _ufoPrefab;
        [SerializeField] private Transform _container;

        private EnemyService _enemyService;
        private UfoViewFactory _factory;

        [Inject]
        public void Construct(EnemyService enemyService)
        {
            _enemyService = enemyService;
        }

        private void Awake()
        {
            _factory = new UfoViewFactory(_ufoPrefab, _container);
        }

        private void Update()
        {
            foreach (var enemy in _enemyService.Enemies)
            {
                if (enemy is UfoModel ufo)
                {
                    _factory.GetOrCreate(ufo);
                }
            }
        }
    }
}