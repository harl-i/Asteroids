using System;
using System.IO;
using Game.Core.World;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Game.Infrastructure.Services
{
    public class ConfigService : IInitializable
    {
        private PlayerConfig _playerConfig;
        private WorldConfig _worldConfig;
        private EnemyConfig _enemyConfig;

        public PlayerConfig PlayerConfig => _playerConfig;
        public WorldConfig WorldConfig => _worldConfig;
        public EnemyConfig EnemyConfig => _enemyConfig;

        public void Initialize()
        {
            Load();
        }

        private void Load()
        {
            _playerConfig = LoadJson<PlayerConfig>("player.json");
            _worldConfig = LoadJson<WorldConfig>("world.json");
            _enemyConfig = LoadJson<EnemyConfig>("enemy.json");
        }

        private T LoadJson<T>(string fileName)
        {
            var path = Path.Combine(Application.streamingAssetsPath, "Configs", fileName);
            //var json = File.ReadAllText(path);

            var json = ReadConfigText(path);

            if (string.IsNullOrEmpty(json))
                throw new InvalidOperationException($"Config file '{fileName}' is empty or missing. Path: {path}");

            return JsonUtility.FromJson<T>(json);
        }

        private static string ReadConfigText(string path)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using var request = UnityWebRequest.Get(path);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
            }

            if (request.result != UnityWebRequest.Result.Success)
                throw new IOException($"Failed to load config from '{path}': {request.error}");

            return request.downloadHandler.text;
#else
            return File.ReadAllText(path);
#endif
        }
    }
}