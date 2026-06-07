using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Game.Core.World;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Game.Infrastructure.Services
{
    public class ConfigService : IInitializable
    {
        private const string PlayerConfigFileName = "player.json";
        private const string WorldConfigFileName = "world.json";
        private const string EnemyConfigFileName = "enemy.json";
        private const string ViewConfigFileName = "view.json";

        private PlayerConfig _playerConfig;
        private WorldConfig _worldConfig;
        private EnemyConfig _enemyConfig;
        private ViewConfig _viewConfig;
        private UniTaskCompletionSource _loadCompletion;
        private bool _isLoading;

        public PlayerConfig PlayerConfig => _playerConfig;
        public WorldConfig WorldConfig => _worldConfig;
        public EnemyConfig EnemyConfig => _enemyConfig;
        public ViewConfig ViewConfig => _viewConfig;
        public bool IsLoaded { get; private set; }

        public void Initialize()
        {
            LoadAsync().Forget();
        }

        public async UniTask LoadAsync()
        {
            if (IsLoaded)
                return;

            if (_isLoading)
            {
                await _loadCompletion.Task;
                return;
            }

            _isLoading = true;
            _loadCompletion = new UniTaskCompletionSource();

            try
            {
                _playerConfig = await LoadJsonAsync<PlayerConfig>(PlayerConfigFileName);
                _worldConfig = await LoadJsonAsync<WorldConfig>(WorldConfigFileName);
                _enemyConfig = await LoadJsonAsync<EnemyConfig>(EnemyConfigFileName);
                _viewConfig = await LoadJsonAsync<ViewConfig>(ViewConfigFileName);
                IsLoaded = true;
                _loadCompletion.TrySetResult();
            }
            catch (Exception exception)
            {
                _loadCompletion.TrySetException(exception);
                throw;
            }
        }

        private async UniTask<T> LoadJsonAsync<T>(string fileName)
        {
            var path = Path.Combine(Application.streamingAssetsPath, "Configs", fileName);
            var json = await ReadConfigTextAsync(path);

            if (string.IsNullOrEmpty(json))
                throw new InvalidOperationException($"Config file '{fileName}' is empty or missing. Path: {path}");

            return JsonUtility.FromJson<T>(json);
        }

        private static async UniTask<string> ReadConfigTextAsync(string path)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using var request = UnityWebRequest.Get(path);
            await request.SendWebRequest().ToUniTask();

            if (request.result != UnityWebRequest.Result.Success)
                throw new IOException($"Failed to load config from '{path}': {request.error}");

            return request.downloadHandler.text;
#else
            return File.ReadAllText(path);
#endif
        }
    }
}
